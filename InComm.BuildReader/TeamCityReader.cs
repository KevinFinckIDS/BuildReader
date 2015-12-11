using System;
using System.Collections.Generic;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;

namespace InComm.BuildReader
{
    public class TeamCityReader
    {
        private string TeamCityServerAddress { get; set; }

        private TeamCityClient _teamCity;
        public TeamCityClient TeamCity 
        {
            get { return _teamCity ?? (_teamCity = GetTeamCityClient()); }
        }

        public TeamCityReader(string teamCityServerAddress)
        {
            TeamCityServerAddress = teamCityServerAddress;
        }

        private TeamCityClient GetTeamCityClient()
        {
            var client = new TeamCityClient(TeamCityServerAddress);
            var creds = new Credentials();
            client.Connect(creds.TeamCityUsername, creds.TeamCityPassword);
            return client;
        }

        public List<TeamCitySharp.DomainEntities.BuildConfig> GetBuildConfigs()
        {
            return TeamCity.BuildConfigs.All();
        }

        public List<TeamCitySharp.DomainEntities.Build> GetBuildsByConfigId(string buildConfigId)
        {
            return TeamCity.Builds.ByBuildConfigId(buildConfigId);
        }
        public List<TeamCitySharp.DomainEntities.Build> GetBuildsByUser(string userName)
        {
            return TeamCity.Builds.ByUserName(userName);
        }
        public List<TeamCitySharp.DomainEntities.Build> GetBuildsSince(DateTime asOf)
        {
            return TeamCity.Builds.AllSinceDate(asOf);
        }

        public List<TeamCitySharp.DomainEntities.User> GetUsers()
        {
            return TeamCity.Users.All();
        }

        public List<TeamCitySharp.DomainEntities.Build> GetUserBuilds(string username)
        {
            return TeamCity.Builds.ByUserName(username);
        }

        public List<Change> GetChanges()
        {
            return TeamCity.Changes.All();
        }
    }
}
