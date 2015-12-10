using System.Collections.Generic;

namespace InComm.BuildReader
{
    public class BuildConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<Build> Builds { get; set; }
    }
}
