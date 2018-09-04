using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullWebappAutomation
{

    class AssertionException : Exception
    {
        public AssertionException()
        {
        }

        public AssertionException(string message)
            : base(message)
        {
        }

        public AssertionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class RetryException : Exception
    {
        public RetryException()
        {
        }

        public RetryException(string message)
            : base(message)
        {
        }

        public RetryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
