using System;
using System.Runtime.Serialization;

namespace DW.ELA.Utility
{
    [Serializable]
    internal class HttpException : Exception
    {
        public readonly int StatusCode;
        public readonly string ReasonPhrase;

        public HttpException()
        {
        }

        public HttpException(string message) : base(message)
        {
        }

        public HttpException(int statusCode, string reasonPhrase)
            : this (GenerateExceptionMessage(statusCode, reasonPhrase))
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public HttpException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GenerateExceptionMessage(int statusCode, string reasonPhrase)
        {
            return $"Error #: {statusCode} - Message: {reasonPhrase}";
        }
    }


}