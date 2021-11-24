using System;

namespace Domain.Exceptions {
    public class LegacyServiceException : Exception {
        public LegacyServiceException() : base() { }
        public LegacyServiceException(string message) : base(message) { }
        public LegacyServiceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
