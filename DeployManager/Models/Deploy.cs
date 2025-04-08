namespace DeployManager.Models
{
    public class Deploy
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int EnvironmentId { get; set; }
        public DeploymentEnvironment Environment { get; set; }

        public DateTime DeployedAt { get; set; }
        public string Status { get; set; }
        public string Responsible { get; set; }
    }
}
