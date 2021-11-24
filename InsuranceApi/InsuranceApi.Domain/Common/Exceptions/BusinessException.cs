using System;

namespace InsuranceApi.Domain.Common.Exceptions {
    public class BusinessException : System.Exception {

        public BusinessException() {
        }
        public BusinessException(string message)
            : base(message) {
        }
        public BusinessException(string message, Exception inner)
            : base(message, inner) {          

        }
    }
}
