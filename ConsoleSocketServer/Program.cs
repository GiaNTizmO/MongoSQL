using System;
using System.Net;
using System.Threading;
class Program
{
    static void Main(string[] args)
    {
        ServicePointManager.DefaultConnectionLimit = 1024;
        ThreadPool.SetMinThreads(128, 256);
        ThreadPool.SetMaxThreads(128, 256);

        var thread = new Thread(() => new Server("0.0.0.0", 3306));
        thread.Start();

        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Сервер запущен.");
    }
}