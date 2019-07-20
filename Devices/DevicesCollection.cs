using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace Devices
{
    public class DevicesCollection
    {
        /// <summary>
        /// 获取所有已添加的设备
        /// </summary>
        public static List<IDevices> Devices
        {
            get
            {
                if (devs == null)
                    devs = GetAddedDevices();
                return devs;
            }
        }
        /// <summary>
        /// 启动所有设备
        /// </summary>
        /// <returns></returns>
        public static void StartAll()
        {
            if (Devices != null)
                foreach (IDevices item in Devices)
                    if (item.State == DevicesState.Closed)
                        item.Open();
        }
        /// <summary>
        /// 关闭所有设备
        /// </summary>
        public static void CloseAll()
        {
            if (devs != null)
                foreach (IDevices item in devs)
                    if (item.State == DevicesState.Opened || item.State == DevicesState.Error)
                        item.Close();
        }

        /// <summary>
        /// 为所有设备添加当有命令执行完时触发的回调
        /// </summary>
        public static event CommandComplete CommandCompleted
        {
            add
            {
                commandCompleted += value;
                if (Devices != null)
                    foreach (IDevices item in Devices)
                        item.CommandCompleted += value;
            }
            remove
            {
                commandCompleted -= value;
                if (Devices != null)
                    foreach (IDevices item in Devices)
                        item.CommandCompleted -= value;
            }
        }
        /// <summary>
        /// 为所有设备添加连接状态状态改变时发生的回调
        /// </summary>
        public static event Action<IDevices, DevicesState> StateChanged
        {
            add
            {
                stateChanged += value;
                if (Devices != null)
                    foreach (IDevices item in Devices)
                        item.StateChanged += value;
            }
            remove
            {
                stateChanged -= value;
                if (Devices != null)
                    foreach (IDevices item in Devices)
                        item.StateChanged -= value;
            }
        }


        #region 私有
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "Devices\\addedDevices.xml";
        private static List<IDevices> devs;
        internal static CommandComplete commandCompleted;
        internal static Action<IDevices, DevicesState> stateChanged;
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="dev"></param>
        public static void Add(IDevices dev)
        {
            if (dev == null)
                return;
            if (devs == null)
                devs = new List<IDevices>();
            devs.Add(dev);
            SaveAddedDevices(devs);
        }
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="dev"></param>
        internal static void Remove(IDevices dev)
        {
            devs.Remove(dev);
            SaveAddedDevices(devs);
        }
        /// <summary>
        /// 获取可以添加的设备
        /// </summary>
        /// <returns></returns>
        public static List<DevicesInformation> GetCanAddDevices()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            Type[] types = ass.GetTypes();
            Type[] paramTypes = new Type[] { typeof(string) };
            List<DevicesInformation> list = new List<DevicesInformation>();
            foreach (var item in types)
                if (item.IsClass && !item.IsAbstract && item.GetInterface("Devices.IDevices") != null)
                {
                    DevicesInformation info = ((IDevices)item.GetConstructor(paramTypes).Invoke(new object[] { null })).Info;
                    info.ClassInfo = item.FullName;
                    list.Add(info);
                }
            return list;
        }
        /// <summary>
        /// 获取已添加的设备
        /// </summary>
        /// <returns></returns>
        private static List<IDevices> GetAddedDevices()
        {
            List<saveConfig> list = Tool.GetObjectByXML<List<saveConfig>>(path);
            if (list == null)
                return null;

            List<IDevices> devs = new List<IDevices>();
            List<saveConfig> scs = Tool.GetObjectByXML<List<saveConfig>>(path);
            Assembly ass = Assembly.GetExecutingAssembly();
            foreach (var item in scs)
            {
                Type type = ass.GetType(item.Info.ClassInfo);
                ConstructorInfo cinfo = type.GetConstructor(new Type[] { typeof(string) });
                IDevices idev = (IDevices)cinfo.Invoke(new object[] { item.ConfigFile });
                idev.Info = item.Info;
                devs.Add(idev);
            }
            return devs;
        }

        /// <summary>
        /// 保存已添加的设备
        /// </summary>
        private static void SaveAddedDevices(List<IDevices> list)
        {
            if ((list == null || list.Count == 0))
            {
                Tool.ObjectSaveToXML(null, path);
                return;
            }
            var saves = list.Select<IDevices, saveConfig>(o => new saveConfig { Info = o.Info, ConfigFile = o.ConfigFileName }).ToList();
            Tool.ObjectSaveToXML(saves, path);
        }
        /// <summary>
        /// 跟据配置创建一个新的设备实例
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        public static IDevices CreateDevices(string configFile, string classInfo)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            Type type = ass.GetType(classInfo);
            ConstructorInfo cinfo = type.GetConstructor(new Type[] { typeof(string) });
            IDevices db = (IDevices)cinfo.Invoke(new object[] { configFile });
            db.Info.ClassInfo = classInfo;
            db.Info.Id = Guid.NewGuid().ToString();
            return (IDevices)db;
        }

        #endregion

    }
    [Serializable]
    public class saveConfig
    {
        public string ConfigFile { get; set; }
        public DevicesInformation Info { get; set; }
    }
}
