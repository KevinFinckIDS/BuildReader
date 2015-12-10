using System;

namespace InComm.BuildReader
{
    class MissingEnvironmentVariableException : Exception
    {
        public MissingEnvironmentVariableException(string variable) : base(string.Format("Required environment variable \"{0}\" has not been defined.", variable)) { }
    }
}
