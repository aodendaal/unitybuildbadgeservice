using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityBuildBadge.Models
{
    public class BuildInfo
    {
        public DateTime ReceivedDate { get; set; }
        public string ProjectName { get; set; }
        public string BuildTargetName { get; set; }
        public Guid ProjectGuid { get; set; }
        public string OrgForeignKey { get; set; }
        public int BuildNumber { get; set; }
        public string BuildStatus { get; set; }
        public string StartedBy { get; set; }
        public string Platform { get; set; }
        public BuildLinks Links { get; set; }
          
    }

    public class BuildLinks
    {
        public BuildAction API_Self { get; set; }
        public BuildAction Dashboard_Url { get; set; }
        public BuildAction Dashboard_Project { get; set; }
        public BuildAction Dashboard_Summary { get; set; }
        public BuildAction Dashboard_Log { get; set; }
    }

    public class BuildAction
    {
        public string Method { get; set; }
        public string HRef { get; set; }
    }
}