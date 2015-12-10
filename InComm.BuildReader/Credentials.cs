using System;

namespace InComm.BuildReader
{
    public class Credentials
    {
        public const string TeamCityUsernameEnvironmentVariable = "TeamCityUsername";
        public const string TeamCityPasswordEnvironmentVariable = "TeamCityPassword";

        private string _teamCityUsername;
        public string TeamCityUsername
        {
            get
            {
                if (_teamCityUsername == null)
                {
                    _teamCityUsername = GetEnvironmentVariable(TeamCityUsernameEnvironmentVariable);
                    if (string.IsNullOrWhiteSpace(_teamCityUsername))
                    {
                        throw new MissingEnvironmentVariableException(TeamCityUsernameEnvironmentVariable);
                    }
                }
                return _teamCityUsername;
            }
            set { _teamCityUsername = value; }
        }

        private string _teamCityPassword;
        public string TeamCityPassword
        {
            get
            {
                if (_teamCityPassword == null)
                {
                    _teamCityPassword = GetEnvironmentVariable(TeamCityPasswordEnvironmentVariable);
                    if (string.IsNullOrWhiteSpace(_teamCityPassword))
                    {
                        throw new MissingEnvironmentVariableException(TeamCityPasswordEnvironmentVariable);
                    }
                }
                return _teamCityPassword;
            }
            set { _teamCityPassword = value; }
        }

        private string GetEnvironmentVariable(string variable)
        {
            try
            {
                return Environment.GetEnvironmentVariable(variable);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
