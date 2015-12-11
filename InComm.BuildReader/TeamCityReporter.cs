using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace InComm.BuildReader
{
    public class TeamCityReporter
    {
        private readonly TeamCityReader _reader;
        private readonly IWriter _writer;

        public TeamCityReporter(TeamCityReader reader, IWriter writer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            _reader = reader;
            _writer = writer;
        }

        public void ShowAll()
        {
            //ShowBuildConfigs();
            ShowBuilds();
            //ShowUsers();
            //ShowUserBuilds("kevinfinck");
            //ShowUserBuilds("bryanjoseph");
            //ShowChanges();
        }

        public void ShowBuildConfigs()
        {
            var configs = _reader.GetBuildConfigs();
            foreach (var sharpConfig in configs)
            {
                var sharpBuilds = _reader.GetBuildsByConfigId(sharpConfig.Id);
                var config = SharpMapper.ToBuildConfig(sharpConfig, sharpBuilds);

                _writer.Write(string.Format(
                    "Build Config:  Name = {0}\t Project = {1}\n{2}", 
                    config.Name, 
                    config.ProjectName,
                    JsonConvert.SerializeObject(config)));

                //ShowBuilds(sharpBuilds);
            }
        }

        public void ShowBuilds()
        {
            ShowBuilds(_reader.GetBuildsSince(DateTime.Now.AddDays(-30)));
        }
        public void ShowBuilds(List<TeamCitySharp.DomainEntities.Build> buildList)
        {
            if (buildList == null)
            {
                return;
            }

            foreach (var sharpBuild in buildList)
            {
                var build = SharpMapper.ToBuild(sharpBuild);
                _writer.Write(string.Format("Status: {0}\t Build: {1}", build.Status, build.Name));
                _writer.Write(JsonConvert.SerializeObject(build));
            }
        }

        public void ShowUsers()
        {
            var users = _reader.GetUsers();
            foreach (var sharpUser in users)
            {
                var user = SharpMapper.ToUser(sharpUser, _reader.GetBuildsByUser(sharpUser.Username));
                _writer.Write(JsonConvert.SerializeObject(user));
            }
        }

        public void ShowUserBuilds(string username)
        {
            var builds = _reader.GetUserBuilds(username);
            _writer.Write(string.Format("{0}, {1} builds:", username, builds.Count()));
            foreach (var build in builds)
            {
                _writer.Write(string.Format("Status: {0}\t Build: {1}", build.Status, build.BuildTypeId));
            }
        }

        public void ShowChanges()
        {
            var changes = _reader.GetChanges();
            _writer.Write("\nChanges:");
            foreach (var change in changes
                    .GroupBy(c => new { c.Username, c.Date })
                    .Select(c => new { c.Key.Username, c.Key.Date, Total = c.Count() })
                    .OrderByDescending(c => c.Date))
            {
                _writer.Write(string.Format("{0:G} {1:##} changes by {2}", change.Date, change.Total, change.Username));
            }
        }
    }
}
