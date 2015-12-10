namespace InComm.BuildReader
{
    public class Build
    {
        public const string SuccessStatus = "SUCCESS";

        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IsSuccess { get { return Status == SuccessStatus; } }
    }
}
