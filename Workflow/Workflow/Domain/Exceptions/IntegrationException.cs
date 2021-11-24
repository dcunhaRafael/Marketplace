﻿using System;

namespace Domain.Exceptions {
    public class IntegrationException : Exception {
        public IntegrationException() : base() { }
        public IntegrationException(string message) : base(message) { }
        public IntegrationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
