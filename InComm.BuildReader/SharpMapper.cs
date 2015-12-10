using System.Collections.Generic;
using System.Linq;

namespace InComm.BuildReader
{
    public static class SharpMapper
    {
        public static BuildConfig ToBuildConfig(TeamCitySharp.DomainEntities.BuildConfig sharpConfig, List<TeamCitySharp.DomainEntities.Build> sharpBuildList)
        {
            return new BuildConfig
            {
                Id = sharpConfig.Id,
                Name = sharpConfig.Name,
                Description = sharpConfig.Description,
                ProjectId = sharpConfig.ProjectId,
                ProjectName = sharpConfig.ProjectName,
                Builds = ToBuildList(sharpBuildList),
            };
        }

        public static List<Build> ToBuildList(List<TeamCitySharp.DomainEntities.Build> sharpList)
        {
            if ((sharpList == null) || (sharpList.Count < 1))
            {
                return new List<Build>();
            }
            return sharpList.Select(ToBuild).ToList();
        }

        public static Build ToBuild(TeamCitySharp.DomainEntities.Build sharpBuild)
        {
            if (sharpBuild == null)
            {
                return new Build();
            }
            return new Build {Id = sharpBuild.Id, Name = sharpBuild.BuildTypeId, Status = sharpBuild.Status};
        }

        public static User ToUser(TeamCitySharp.DomainEntities.User sharpUser, List<TeamCitySharp.DomainEntities.Build> sharpBuildList)
        {
            return new User
            {
                Id = sharpUser.Id,
                Name = sharpUser.Name,
                Username = sharpUser.Username,
                Email = sharpUser.Email,
                Builds = ToBuildList(sharpBuildList),
            };
        }
    }
}
