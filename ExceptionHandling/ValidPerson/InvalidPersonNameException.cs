using System;

namespace ValidPerson
{
    public class InvalidPersonNameException : Exception
    {
        public InvalidPersonNameException(string msg)
            : base (msg)
        {

        }
    }
}
