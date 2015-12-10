using System.Collections.Generic;

namespace InComm.BuildReader
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Build> Builds { get; set; }
    }
}
