using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Util.Mail {
    public class EmailSettings {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
