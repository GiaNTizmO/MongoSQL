using System;
using System.Collections.Generic;
using System.Text;

namespace MongoSQL.Commands.Core
{
    public static class CommandExecuter
    {
        public static void SQLExecutor(string SQL)
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "EXECUTING SQL COMMAND: " + SQL);
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Converting SQL Command to MongoDB Compatible command...");
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + new NotImplementedException().ToString());
        }
    }
}