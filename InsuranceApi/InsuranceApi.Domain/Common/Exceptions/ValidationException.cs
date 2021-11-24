using System;
using System.Collections.Generic;

namespace InsuranceApi.Domain.Common.Exceptions {
    public class ValidationException : System.Exception {

        public List<string> messageList;

        public ValidationException(string message) {
            this.messageList = new List<string>() {
                 message
            };
        }

        public ValidationException(List<string> messageList) {
            this.messageList = messageList;
        }

        public string GetMessages() {
            return String.Join(";", messageList);
        }
    }
}