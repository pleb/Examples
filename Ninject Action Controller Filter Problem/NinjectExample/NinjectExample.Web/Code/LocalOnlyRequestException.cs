using System;
using System.Runtime.Serialization;

namespace NinjectExample.Web.Code
{
    [Serializable]
    public class LocalOnlyRequestException : InvalidOperationException
    {
        private const string DefaultMessage = "The requested action cannot be processed as the this action has been marked as only available to local requests.";

        public LocalOnlyRequestException()
            : base(DefaultMessage)
        {
        }

        public LocalOnlyRequestException(string message)
            : base(message)
        {
        }

        public LocalOnlyRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected LocalOnlyRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}