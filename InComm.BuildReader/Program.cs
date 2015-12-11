using System;
using System.Linq;

namespace InComm.BuildReader
{
    class Program
    {
        // NOTE: Authentication is done by using to environment variables: 
        //  * TeamCityUsername
        //  * TeamCityPassword
        // After setting these, you may need to restart Visual Studio to pick them up.


        //private const string TeamCityServerAddress = "10.20.30.205:8080";
        private const string TeamCityServerAddress = "tc-master.gc.local:8080";
        private const string ElasticSearchAddress = "http://10.3.29.129:9200";

        static void Main(string[] args)
        {
            ShowTeamCityData();                 // This will just display build info from TeamCity
            //UploadTeamCityToElasticsearch();  // This will upload to Elasticsearch 

            Console.Write("Press [Enter] to exit.");
            Console.ReadLine();
        }

        private static void ShowTeamCityData()
        {
            new TeamCityReporter(
                new TeamCityReader(TeamCityServerAddress), 
                new ConsoleWriter())
                .ShowAll();
        }

        private static void UploadTeamCityToElasticsearch()
        {
            var reader = new TeamCityReader(TeamCityServerAddress);
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

            Console.Write("Uploading {0} build configs...", configs.Count);

            new ElasticLoader(ElasticSearchAddress)
                .UploadList(SharpMapper.ToBuildConfigList(configs, reader), "teamcity_buildconfigs", "BuildConfig");

            Console.WriteLine();
        }

        private static void UploadUsers(TeamCityReader reader)
        {
            var sharpUsers = reader.GetUsers();
            if ((sharpUsers == null) || (sharpUsers.Count < 1))
            {
                return;
            }

            Console.Write("Uploading {0} user records...", sharpUsers.Count);
            var users = sharpUsers
                .Select(sharpUser => 
                    SharpMapper.ToUser(
                        sharpUser, 
                        reader.GetBuildsByUser(sharpUser.Username)))
                .ToList();

            new ElasticLoader(ElasticSearchAddress).UploadList(users, "teamcity_users", "User");
            Console.WriteLine();
        }

    }
}
