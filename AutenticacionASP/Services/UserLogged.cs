using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionASP.Models.ServiceMessage
{
    public class UserLogged
    {
        public string FullName { get; set; }
        public string MobilePhone { get; set; }
        public int SMSCode { get; set; }
        public int SMSCodeVerify { get; set; }

        public bool IsLogged
        {
            get
            {
                return !string.IsNullOrWhiteSpace(FullName);
            }
        }

    }
}
