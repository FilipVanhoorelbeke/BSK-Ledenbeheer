using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSKLedenManagement.Models
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public string User { get; set; }
    }
}