using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSKLedenManagement.Helpers;

namespace BSKLedenManagement.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MailSender.SendMail("Test", "Test", "filip.vanhoorelbeke@gmail.com");
        }
    }
}
