using System;

namespace WorldOfRaces.Exceptions
{
    public class NotEnoughElexirsException : Exception
    {
        public NotEnoughElexirsException(string msg)
            : base(msg)
        {
        }
    }
}
