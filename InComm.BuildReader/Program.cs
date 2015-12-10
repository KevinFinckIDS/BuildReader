using System;
using System.Collections.Generic;

namespace InComm.BuildReader
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowTeamCityData();
            UploadTeamCityToElasticsearch();
        }

        private static void ShowTeamCityData()
        {
            new TeamCityReporter(null, new ConsoleWriter()).ShowAll();
            Console.Write("Press [Enter] to exit.");
            Console.ReadLine();
        }

        private static void UploadTeamCityToElasticsearch()
        {
            var reader = new TeamCityReader();
            UploadBuildConfigs(reader);
            UploadUsers(reader);
        }

        private static void UploadBuildConfigs(TeamCityReader reader)
        {
            var configs = reader.GetBuildConfigs();
            if (configs == null)
            {
                return;
            }

            ElasticLoader.UploadList(SharpMapper.ToBuildConfigList(configs, reader), "teamcity_buildconfigs", "BuildConfig");
        }

        private static void UploadUsers(TeamCityReader reader)
        {
            var configs = reader.GetBuildConfigs();
            if (configs == null)
            {
                return;
            }

            var sharpUsers = reader.GetUsers();
            if ((sharpUsers == null) || (sharpUsers.Count < 1))
            {
                return;
            }

            var users = new List<User>();
            foreach (var sharpUser in sharpUsers)
            {
                users.Add(SharpMapper.ToUser(sharpUser, reader.GetBuildsByUser(sharpUser.Username)));
            }

            ElasticLoader.UploadList(users, "teamcity_users", "User");
        }

    }
}
