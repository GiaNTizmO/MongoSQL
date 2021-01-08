using MongoSQL.Commands.Core;
using MongoSQL.Configs;
using System;
using System.Net;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        string buildversion = " 0.12.34-1";
        string builddate = DateTime.Now.ToShortDateString();

        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @" __  __                         _____  ____  _      ");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @"|  \/  |                       / ____|/ __ \| |     ");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @"| \  / | ___  _ __   __ _  ___| (___ | |  | | |     ");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @"| |\/| |/ _ \| '_ \ / _` |/ _ \\___ \| |  | | |     ");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @"| |  | | (_) | | | | (_| | (_) |___) | |__| | |____ ");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @"|_|  |_|\___/|_| |_|\__, |\___/_____/ \___\_\______|");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @"                     __/ |                          ");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + @"                    |___/                           ");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "MongoSQL Alpha");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Build: " + buildversion);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Build date: " + builddate);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Copyright(c) 2000, 2018, 2021, Oracle, MariaDB Corporation Ab and others, MongoDB Contributors");
        Thread.Sleep(1000);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "=======================================================");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Initializing Configuration...");
        ConfigManager.CheckConfiguration();

        int connectionlimit = GlobalConfig.connectionLimit;
        int minthreads_workerthreads = GlobalConfig.minimalWorkerThreads;
        int minthreads_completionportthreads = GlobalConfig.minimalComplettionWorkerThreads;
        int maxthreads_workerthreads = GlobalConfig.maximalWorkerThreads;
        int maxthreads_completionportthreads = GlobalConfig.maximalComplettionWorkerThreads;
        string serverIP = GlobalConfig.serverIP;
        int serverPort = GlobalConfig.serverPort;

        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Preparing startup...");
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Setting connection limit to: " + connectionlimit);
        ServicePointManager.DefaultConnectionLimit = connectionlimit;
        Thread.Sleep(100);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Setting threads worker threads to: " + minthreads_workerthreads);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Setting threads completion port threads to: " + minthreads_completionportthreads);
        ThreadPool.SetMinThreads(128, 256);
        Thread.Sleep(100);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Setting threads worker threads to: " + maxthreads_workerthreads);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Setting threads completion port threads to: " + maxthreads_completionportthreads);
        ThreadPool.SetMaxThreads(128, 256);
        Thread.Sleep(100);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Prepare startup finished!");
        Thread.Sleep(1000);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Starting server at: " + serverIP + ":" + serverPort);
        var thread = new Thread(() => new Server(serverIP, serverPort));
        thread.Start();
        Thread.Sleep(2000);
        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Server started!");
        Thread.Sleep(2000);
        while (true)
        {
            Console.Write("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "[MongoSQL Server]$ ");
            string cin = Console.ReadLine();
            if (cin == "help")
                Help.help();
            else if (cin == "info")
                Information.info();
            else if (cin == "shutdown")
                Shutdown.ShutdownVoid();
            else if (cin.StartsWith("sqlexecute"))
            {
                try
                {
                    cin = cin.Remove(0, 11);
                }
                catch (ArgumentOutOfRangeException) { }
                CommandExecuter.SQLExecutor(cin);
            }
            else if (cin == "status")
                Status.StatusCmd();
            else if (String.IsNullOrEmpty(cin))
                continue;
            else
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + cin + " : Unknown Command. Type \"help\" to get all available commands!");
        }
    }
}