using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MongoSQL.Commands.Core
{
    public static class Shutdown
    {
        public static void ShutdownVoid()
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "MongoSQL is about to shutdown!");
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Limiting Threads...");
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(1, 1);
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Closing...");
            Environment.Exit(0);
        }
    }
}