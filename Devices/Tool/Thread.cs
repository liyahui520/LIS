using System;
using System.Collections.Generic;
using System.Threading;
namespace Devices
{
    internal class HIHSThread
    {
        private Thread thread;
        private AutoResetEvent autoReset;
        private Queue<Action> las;
        private bool run = true;
        private object lockobj = true;
        public HIHSThread()
        {
            autoReset = new AutoResetEvent(true);
            las = new Queue<Action>();
            thread = new Thread(() =>
            {
                while (run)
                {
                    if (las.Count > 0)
                        las.Dequeue().Invoke();
                    else
                        autoReset.WaitOne();
                }
            });

        }


        public void QueueWork(Action ac)
        {
            if (thread == null)
                throw new System.Exception("线程已被回收");
            if (thread.ThreadState == ThreadState.Unstarted)
                thread.Start();
            lock (las)
            {
                las.Enqueue(ac);
            }
            while (autoReset == null) ;
            autoReset.Set();
        }

        public void Exit()
        {
            if (thread == null)
                return;
            if (autoReset == null)
                return;
            run = false;
            if (thread.ThreadState == ThreadState.WaitSleepJoin)
                autoReset.Set();
        }
    }

}
