using System;
using System.Collections.Generic;
using System.Text;

namespace MongoSQL.Commands.Core
{
    public static class Status
    {
        public static void StatusCmd()
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + new NotImplementedException().ToString());
        }
    }
}