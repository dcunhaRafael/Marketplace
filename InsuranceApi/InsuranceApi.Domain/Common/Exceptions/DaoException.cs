using System;

namespace InsuranceApi.Domain.Common.Exceptions
{
    public class DaoException : Exception
    {
        public DaoException(string message)
        : base(message)
        {
        }

        public DaoException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
