using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
namespace Devices
{
    /// <summary>
    /// 设备
    /// </summary>
    [Serializable]
    public abstract class DevicesBase<T> : IDevices where T : Config
    {
        protected string configFileName;
        protected Log log;
        private Action<IDevices, DevicesState> stateChanged;
        private List<Command> cmds;
        private event CommandComplete commandComplete;
        private DevicesState state = DevicesState.Closed;
        private string cmdsFile;
        private Exception err;
        private DevicesInformation info;
        private static IPrint print;


        #region 属性
        protected IProtocol Protocol { get; set; }
        public Config Config { get; set; }
        public List<Command> CMDS
        {
            get { return cmds; }
            set { cmds = value; }
        }
        /// <summary>
        /// 设备信息
        /// </summary>
        public virtual DevicesInformation Info
        {

            get { if (info == null) info = DefaultInfo(); return info; }
            set { info = value; }
        }
        /// <summary>
        /// 获取设备状态
        /// </summary>
        public DevicesState State
        {
            get { return state; }
            protected set
            {
                state = value;
                stateChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// 当前错误
        /// </summary>
        public Exception Error
        {
            get { return err; }
            protected set { err = value; }
        }


        public virtual IPrint PrintTool
        {
            get
            {
                if (print == null)
                    print = Tool.GetObjectByClass<IPrint>("Print.dll", "Devices.Print.UniversalPrint");
                return print;
            }
        }
        #endregion;

        #region 公有方法

        /// <summary>
        /// 配置文件地址
        /// </summary>
        public string ConfigFileName { get { return configFileName; } }

        public DevicesBase(string _cgFile)
        {
            configFileName = _cgFile;
            if (string.IsNullOrEmpty(configFileName))
                configFileName = string.Format("{0}Devices\\{1}\\{2}.xml", AppDomain.CurrentDomain.BaseDirectory, Info.Brand, DateTime.Now.ToString("yyyyMMddHHmmss"));
            if (!string.IsNullOrEmpty(ConfigFileName) && System.IO.File.Exists(configFileName))
                Config = Tool.GetObjectByXML<T>(configFileName);
            else
                Config = DefaultConfig();


            cmdsFile = string.Format("{0}Devices\\cmds\\{1}", AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(configFileName));
            if (!Directory.Exists(Path.GetDirectoryName(cmdsFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(cmdsFile));
            if (File.Exists(cmdsFile))
                cmds = Tool.GetObjectByXML<List<Command>>(cmdsFile);

            log = new Log(Info.Name);
        }

        /// <summary>
        /// 向设备发送命令,同一个设备不可以存在命令ID相同的命令
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool AddCommand(Command cmd)
        {
            AddCmd(cmd);
            return true;
        }

        /// <summary>
        /// 取消一个命令
        /// </summary>
        /// <param name="cmdId"></param>
        /// <returns></returns>
        public virtual bool RemoveCommand(string cmdId)
        {
            if (cmds == null)
                return false;
            var cmd = cmds.LastOrDefault(o => o.Id == cmdId);
            if (cmd == null)
                return false;
            log.Write("删除命令:" + cmd.ToString());
            lock (cmds)
            {
                RemoveCmd(cmd);
            }
            return true;
        }

        /// <summary>
        /// 获取所有未完成的命令的副本
        /// </summary>
        public virtual List<Command> GetCommandes()
        {
            if (cmds == null || cmds.Count == 0)
                return null;
            Command[] copy = new Command[cmds.Count];
            cmds.CopyTo(copy);
            return copy.ToList();

        }
        public void Restart()
        {
            if (state == DevicesState.Opened || state == DevicesState.Error)
                Close();
            Open();
        }

        public void Open()
        {
            if (!Start())
                System.Windows.Forms.MessageBox.Show(string.Format("{1}{0}未能成功启动！", this.Info.Name, err == null ? "" : err.Message));
        }


        /// <summary>
        /// 关闭通信通道,之后设备不会再上传任何数据
        /// </summary>
        public virtual void Close()
        {
            if (Protocol == null)
                Protocol.Close();
            log.Write("设备关闭");
            State = DevicesState.Closed;
        }

        public abstract System.Windows.Forms.DialogResult ShowConfigForm();

        public virtual void ShowQueueForm()
        {
            DevicesForm form = new DevicesForm();
            Control.UCQueue ucqueue = new Control.UCQueue(this);
            ucqueue.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Text = Info.Name + "检查队列";
            form.Width = 800;
            form.Height = 400;
            form.Controls.Add(ucqueue);
            CommandsChanged += ucqueue.DevCommandsChanged;
            form.FormClosing += (o, e) => CommandsChanged -= ucqueue.DevCommandsChanged;
            form.Show();
        }

        protected virtual DevicesInformation DefaultInfo()
        {
            return new DevicesInformation
            {
                Num = 0,
                Brand = "0",
                Model = "0",
                Name = "0",
                Remarks = "0",
                Code = "0",
                Url = "http://www.baidu.com"
            };
        }

        protected virtual Config DefaultConfig()
        {
            return null;
        }

        public virtual event Action<IDevices, DevicesState> StateChanged
        {
            add { stateChanged += value; }
            remove { stateChanged -= value; }
        }
        ///// <summary>
        ///// 启动该设备
        ///// </summary>
        //public virtual bool Start()
        //{
        //    return true;
        //}
        ///// <summary>
        ///// 关闭设备
        ///// </summary>
        //public virtual void Shutdown()
        //{ }
        public void ResultComplete(Result result)
        {
            DB.Save(result);
            log.Write("接收到数据:" + result.Source);
            RemoveCmd(result.CMD);
            commandComplete?.Invoke(result.CMD, result);
        }

        /// <summary>
        /// 当命令完成时
        /// </summary>
        public virtual event CommandComplete CommandCompleted
        {
            add
            {
                commandComplete += value;
            }
            remove
            {
                commandComplete -= value;
            }
        }

        public virtual void SaveCmds()
        {
            Tool.ObjectSaveToXML(cmds, cmdsFile);
        }

        public virtual void SaveConfig()
        {
            Tool.ObjectSaveToXML(Config, configFileName);
        }

        #endregion 

        #region 私有


        private Action<IDevices, Command> commandsChanged;
        internal event Action<IDevices, Command> CommandsChanged
        {
            add { commandsChanged += value; }
            remove { commandsChanged -= value; }
        }

        private void AddCmd(Command cmd, bool isR = false)
        {
            if (cmd == null)
                return;
            if (cmds == null)
                cmds = new List<Command>();

            log.Write("添加命令:" + cmd.ToString());
            cmds.Add(cmd);
            commandsChanged?.Invoke(this, cmd);
            SaveCmds();
        }
        private void RemoveCmd(Command cmd)
        {
            if (cmd == null)
                return;
            if (cmds.Remove(cmd))
            {
                commandsChanged?.Invoke(this, cmd);
                SaveCmds();
            }
        }



        /// <summary>
        /// 连接到设备并打开通信通道
        /// </summary>
        protected virtual bool Start()
        {
            if (State == DevicesState.Opened || State == DevicesState.Error)
                return true;
            if (Protocol == null)
                return false;

            if (!Protocol.Start())
            {
                Error = Protocol.Error;
                return false;
            }
            State = DevicesState.Opened;
            log.Write("设备启动");
            return true;
        }
        #endregion
    }

    public delegate void CommandComplete(Command sender, Devices.Result e);
}
