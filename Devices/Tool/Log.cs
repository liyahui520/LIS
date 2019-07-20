using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Devices
{
    public class Log
    {
        private string LogPath;
        private string MsgPath;
        private string ErrorPath;
        private byte[] Enter;
        private byte[] Dividing;
        /// <summary>
        /// log文件读写线程,所有的log文件读写在此线程中运行
        /// </summary>
        private static HIHSThread thread;

        static Log()
        {
            thread = new HIHSThread();
            System.Windows.Forms.Application.ApplicationExit += (s, o) => Exit();
        }

        public Log(string devName)
        {
            LogPath = AppDomain.CurrentDomain.BaseDirectory + "Devices\\log\\" + DateTime.Now.ToString("yyyyMM") + "\\";
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
            MsgPath = LogPath + devName + "_" + DateTime.Now.ToString("dd") + "_msg.txt";
            ErrorPath = LogPath + devName + "_" + DateTime.Now.ToString("dd") + "_error.txt";
            Dividing = UTF8Encoding.UTF8.GetBytes("----------------------------------------\r\n");
            Enter = UTF8Encoding.UTF8.GetBytes("\r\n\r\n");
            //if (thread == null)
            //    thread = new HIHSThread();
        }
        public void Error(string text, Exception ex)
        {
            Error(text + ex.StackTrace);
        }

        public void Error(Exception ex)
        {
            Error(ex.StackTrace);
        }

        public void Error(string text)
        {
            WriteLog(ErrorPath, UTF8Encoding.UTF8.GetBytes(text));
        }

        public void Msg(string text)
        {
            WriteLog(MsgPath, UTF8Encoding.UTF8.GetBytes(text));
        }

        public void Write(string text)
        {
            Msg(text);
        }

        public void Write(string text, string titel)
        {
            Msg(titel + ":\r\n" + text);
        }

        public void Write(string text, string titel, LogType logType)
        {
            switch (logType)
            {
                case LogType.Error:
                    Error(titel + ":\r\n" + text);
                    break;
                case LogType.Msg:
                    Msg(titel + ":\r\n" + text);
                    break;
            }
        }

        private void WriteLog(string path, byte[] data)
        {
            //将写件操作添加到队列
            thread.QueueWork(() =>
            {
                using (FileStream fs = File.Open(path, FileMode.OpenOrCreate))
                {
                    fs.Position = fs.Length;
                    string title = DateTime.Now.ToString("HH:mm:ss");
                    byte[] titlebuffer = System.Text.UTF8Encoding.UTF8.GetBytes(title);
                    fs.Write(titlebuffer, 0, titlebuffer.Length);
                    fs.Write(Dividing, 0, Dividing.Length);
                    fs.Write(data, 0, data.Length);
                    fs.Write(Enter, 0, Enter.Length);
                }
            });
        }

        public string Read(LogType logType = LogType.Error)
        {
            string logpath = ErrorPath;
            if (logType == LogType.Msg)
                logpath = MsgPath;
            if (File.Exists(logpath))
            {
                try
                {
                    return File.ReadAllText(logpath, UTF8Encoding.UTF8);
                }
                finally
                {

                }
            }
            return null;
        }
        private static void Exit()
        {
            thread.Exit();
        }

    }

    public enum LogType
    {
        Error = 1,
        Msg = 2,
    }
}
