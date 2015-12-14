using System;

namespace WorldOfRaces.Exceptions
{
    public class ObjectOutsideOfMapException : Exception
    {
        public ObjectOutsideOfMapException(string message)
            : base(message)
        {
        }
    }
}
