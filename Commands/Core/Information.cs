using System;
using System.Collections.Generic;
using System.Text;

namespace MongoSQL.Commands.Core
{
    public static class Information
    {
        public static void info()
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + new NotImplementedException().ToString());
        }
    }
}