using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BSKLedenManagement.Models;

namespace BSKLedenManagement.Helpers
{
    public static class Logger
    {
        public static void Log(LogType type, string user, string message)
        {
            var log = new Log
            {
                LogTime = DateTime.Now,
                Message = message,
                Type = type,
                User = user
            };
            using (var context = new ApplicationDbContext())
            {
                context.Logs.Add(log);
                context.SaveChanges();
            }
        }
    }
}