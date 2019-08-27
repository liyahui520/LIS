using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Devices
{
    public interface IDevices
    {
        /// <summary>
        /// 设备状态
        /// </summary>
        DevicesState State { get; }

        /// <summary>
        /// 设备基本信息
        /// </summary>
        DevicesInformation Info { get; set; }

        /// <summary>
        /// 当有命令执行完时触发
        /// </summary>
        event CommandComplete CommandCompleted;

        /// <summary>
        /// 连接状态状态改变时发生
        /// </summary>
        event Action<IDevices, DevicesState> StateChanged;

        /// <summary>
        /// 打开配置页面
        /// </summary>
        /// <returns></returns>
        System.Windows.Forms.DialogResult ShowConfigForm();

        /// <summary>
        /// 打开队列页面
        /// </summary>
        void ShowQueueForm();

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        bool AddCommand(Command cmd);

        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="cmdId"></param>
        /// <returns></returns>
        bool RemoveCommand(string cmdId);

        /// <summary>
        /// 命令队列
        /// </summary>
        /// <returns></returns>
        List<Command> GetCommandes();

        /// <summary>
        /// 重启设备
        /// </summary>
        void Restart();

        /// <summary>
        /// 连接设备
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭设备连接
        /// </summary>
        void Close();

        /// <summary>
        /// 获取设置配置
        /// </summary>
        Config Config { get; set; }

        /// <summary>
        /// 获取设置Command
        /// </summary>
        List<Command> CMDS { get; set; }

        /// <summary>
        /// 设置配置文件路径
        /// </summary>
        string ConfigFileName { get; }

        /// <summary>
        /// 保存配置
        /// </summary>
        void SaveConfig();

        /// <summary>
        /// 保存command
        /// </summary>
        void SaveCmds();

        IPrint PrintTool { get; set; }
    }
}
