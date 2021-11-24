using System;

namespace Domain.Entities {
    public class Audit {
        public int Id { get; set; }
        public string Level { get; set; }
        public DateTime ActionDate { get; set; }
        public string FeatureName { get; set; }
        public string ActionName { get; set; }
        public string Url { get; set; }
        public string IpAddress { get; set; }
        public string BrowserName { get; set; }
        public string OsName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public int? UserTypeId { get; set; }
        public int? UserId { get; set; }
    }
}
