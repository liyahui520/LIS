using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Timers;
using System.Threading;
namespace Devices
{

    internal class Tool
    {
        private static List<QueueWork> works;
        private static System.Timers.Timer timer;
        internal static void ObjectSaveToXML(object obj, string xmlpath)
        {
            if (obj == null && File.Exists(xmlpath))
            {
                File.Delete(xmlpath);
                return;
            }
            string path = Path.GetDirectoryName(xmlpath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (FileStream fs = File.Create(xmlpath))
            {
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                xml.Serialize(fs, obj);
            }
        }
        internal static T GetObjectByXML<T>(string xmlpath)
        {
            if (!File.Exists(xmlpath))
                return default(T);
            try
            {
                using (FileStream fs = File.Open(xmlpath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    return (T)xml.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        internal static void ObjectSaveToFile(object obj, string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                format.Serialize(fs, obj);
            }
        }
        internal static T GetObjectByFile<T>(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)format.Deserialize(fs);
            }
        }


        internal static T GetObjectByClass<T>(string dllName, string typeName)
        {
            string dll=AppDomain.CurrentDomain.BaseDirectory  + dllName;
            if (!File.Exists(dll))
                return default(T);
            try
            {
                System.Reflection.Assembly assembly= System.Reflection.Assembly.LoadFile(dll);
                return (T)assembly.CreateInstance(typeName);
            }
            catch (Exception)
            {
               
            }
            return default(T);
        }


        internal static void QueueUserWorkItem(int wait, WaitCallback action)
        {
            works.Add(new QueueWork { Time = wait, Callback = action });
            timer.Enabled = true;
        }
        static Tool()
        {
            //works = new List<QueueWork>();
            //timer = new System.Timers.Timer(1000);
            //timer.Enabled = false;
            //timer.Elapsed += (o, e) =>
            //{
            //    lock (works)
            //    {
            //        int length = works.Count;
            //        for (int i = 0; i < length; )
            //        {
            //            works[i].Time -= 1000;
            //            if (works[i].Time <= 0)
            //            {
            //                ThreadPool.QueueUserWorkItem(works[i].Callback);
            //                works.RemoveAt(i);
            //                length--;
            //                continue;
            //            }
            //            i++;
            //        }
            //        if (works.Count == 0)
            //            timer.Enabled = false;
            //    }
            //};
        }
        private class QueueWork
        {
            public int Time { get; set; }
            public WaitCallback Callback { get; set; }
        }


        public static void Log()
        { 
        
        }
    }
}
