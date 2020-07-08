using System;

namespace Library.Exceptions
{
    [Serializable]
    public class BsonIdNotConvertException : Exception
    {
        public BsonIdNotConvertException(string message) : base(message)
        {
        }
    }
}