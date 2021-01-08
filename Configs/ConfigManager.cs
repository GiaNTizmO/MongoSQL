using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MongoSQL.Configs
{
    internal class Config
    {
        public string serverIP;
        public int serverPort;
        public int authMethod;
        public int connectionLimit;
        public int minimalWorkerThreads;
        public int minimalComplettionWorkerThreads;
        public int maximalWorkerThreads;
        public int maximalComplettionWorkerThreads;
        public string mongodbConnectionString;
    }

    public static class GlobalConfig
    {
        public static string serverIP;
        public static int serverPort;
        public static int authMethod;
        public static int connectionLimit;
        public static int minimalWorkerThreads;
        public static int minimalComplettionWorkerThreads;
        public static int maximalWorkerThreads;
        public static int maximalComplettionWorkerThreads;
        public static string mongodbConnectionString;
    }

    public static class ConfigManager
    {
        public static void CheckConfiguration()
        {
            if (!File.Exists("configuration.json"))
            {
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Configuration file does not exists! Creating default configuration...");
                CreateDefaultConfig();
                CheckConfiguration();
            }
            else
            {
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Loading Configuration...");
                Config JSONConfig = new Config();
                using (StreamReader sr = new StreamReader("configuration.json"))
                {
                    try
                    {
                        dynamic JSONConfigFile = JsonConvert.DeserializeObject(sr.ReadToEnd());

                        GlobalConfig.serverIP = JSONConfigFile.serverIP;
                        GlobalConfig.serverPort = JSONConfigFile.serverPort;
                        GlobalConfig.authMethod = JSONConfigFile.authMethod;
                        GlobalConfig.connectionLimit = JSONConfigFile.connectionLimit;
                        GlobalConfig.minimalWorkerThreads = JSONConfigFile.minimalWorkerThreads;
                        GlobalConfig.minimalComplettionWorkerThreads = JSONConfigFile.minimalComplettionWorkerThreads;
                        GlobalConfig.maximalWorkerThreads = JSONConfigFile.maximalWorkerThreads;
                        GlobalConfig.maximalComplettionWorkerThreads = JSONConfigFile.maximalComplettionWorkerThreads;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException rbe)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "ERROR Parsing Configuration file! Recreating...");
                        RecreateConfig();
                    }
                }
            }
        }

        public static void RecreateConfig()
        {
            try
            {
                File.Delete("configuration.json");
                CheckConfiguration();
            }
            catch (IOException IOe)
            {
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Error while deleting config file: " + IOe);
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Server will be terminated!");
                Environment.Exit(-1);
            }
        }

        public static void CreateDefaultConfig()
        {
            Config DefaultConfig = new Config();
            DefaultConfig.serverIP = "0.0.0.0";
            DefaultConfig.serverPort = 3306;
            DefaultConfig.authMethod = 0;
            DefaultConfig.connectionLimit = 1024;
            DefaultConfig.minimalWorkerThreads = 128;
            DefaultConfig.minimalComplettionWorkerThreads = 256;
            DefaultConfig.maximalWorkerThreads = 128;
            DefaultConfig.maximalComplettionWorkerThreads = 256;
            DefaultConfig.mongodbConnectionString = "mongodb:// or mongodb+srv:// type here!";
            string JSONresult = JsonConvert.SerializeObject(DefaultConfig);
            using (var tw = new StreamWriter("configuration.json", true))
            {
                tw.WriteLine(JSONresult.ToString());
                tw.Close();
            }
        }
    }
}