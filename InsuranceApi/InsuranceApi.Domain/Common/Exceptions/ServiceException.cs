using InsuranceApi.Domain.BusinessObjects;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Reflection;

namespace InsuranceApi.Domain.Common.Exceptions {
    public class ServiceException : Exception {


        public ServiceException(string message)
         : base(message) {
        }

        public ServiceException(string message, Exception inner)
            : base(message, inner) {
          
        }

        public ServiceException(string message, Exception inner,object parameter)
            : base(message, inner) {
          
            
        }

        public ServiceException(ProviderEnum provider, string message, MethodBase method, object requestData, object responseData)
          : base(message) { }
        }
}