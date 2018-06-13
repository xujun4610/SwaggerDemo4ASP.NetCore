using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.SwaggerForASPDotNetCore.BO
{
    public class User
    {
        public string AccountName { get; set; }
        public string OSName { get; set; }
        public int RAM_MB { get; set; }
        public string CPUName { get; set; }
        public float CPUClock { get; set; }
        public bool IsEnabledJavaScript { get; set; }

    }
}
