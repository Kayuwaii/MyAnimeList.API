using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace MyAnimeList.API
{
    [Serializable]
    internal class MALAPIException : Exception
    {
        public HttpResponseMessage response;

        public MALAPIException()
        {
        }

        public MALAPIException(HttpResponseMessage response)
        {
            this.response = response;
        }

        public MALAPIException(string message) : base(message)
        {
        }

        public MALAPIException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MALAPIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}