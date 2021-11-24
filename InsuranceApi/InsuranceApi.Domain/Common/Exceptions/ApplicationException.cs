﻿using System;

namespace InsuranceApi.Domain.Common.Exceptions {
    public class ApplicationException : Exception {
        public ApplicationException() {
        }
        public ApplicationException(string message)
            : base(message) {
        }
        public ApplicationException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
