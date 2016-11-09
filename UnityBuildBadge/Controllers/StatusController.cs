using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.Documents.Client;

namespace UnityBuildBadge.Controllers
{
    public class StatusController : ApiController
    {
        private enum BuildState
        {
            Pending,
            Passing,
            Failing,
            Unknown
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(string id)
        {
            var projectId = Guid.Parse(id);

            var buildState = GetBuildState(projectId);

            var subject = "build";
            var status = string.Empty;
            var color = string.Empty;

            switch (buildState)
            {
                case BuildState.Pending:
                    status = "pending";
                    color = "lightgrey";
                    break;

                case BuildState.Passing:
                    status = "passing";
                    color = "brightgreen";
                    break;

                case BuildState.Failing:
                    status = "failing";
                    color = "red";
                    break;
                case BuildState.Unknown:
                    status = "unknown";
                    color = "lightgrey";
                    break;
            }

            var badge = await GetBadge(subject, status, color);

            var response = new HttpResponseMessage();

            response.Content = new ByteArrayContent(badge);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/svg+xml");

            return response;
        }

        private static async Task<byte[]> GetBadge(string subject, string status, string color)
        {
            var client = new HttpClient();

            var result = await client.GetAsync($"https://img.shields.io/badge/{subject}-{status}-{color}.svg");

            var content = await result.Content.ReadAsByteArrayAsync();

            return content;
        }

        private BuildState GetBuildState(Guid projectId)
        {
            var url = "https://unitybadges.documents.azure.com:443/";
            var key = "ayrfZgmGkAuKf1NDubwnEoStE0RWjwqxfxwLG1UkAkAe41tufDDOHeztpCnwfKwmnRL6t3Y3Asmbhtxxrzu0fg==";

            var client = new DocumentClient(new Uri(url), key);

            var database = "UnityBadges";
            var collection = "Messages";

            var query = client.CreateDocumentQuery<Models.BuildInfo>(UriFactory.CreateDocumentCollectionUri(database, collection))
                .Where((buildInfo) => buildInfo.ProjectGuid == projectId);
            //.OrderByDescending((buildInfo) => buildInfo.ReceivedDate)
            //.FirstOrDefault();

            var result = query.ToList();

            var latestInfo = result.OrderByDescending((buildInfo) => buildInfo.ReceivedDate)
                .FirstOrDefault();

            if (latestInfo != null)
            {
                BuildState buildState = BuildState.Unknown;

                switch (latestInfo.BuildStatus)
                {             
                    case "queued":
                    case "started":
                    case "restarted":
                        buildState = BuildState.Pending; break;
                    case "success":
                        buildState = BuildState.Passing; break;
                    case "failure":
                    case "cancelled":
                        buildState = BuildState.Failing; break;
                }

                return buildState;
            }
            else
            {
                return BuildState.Unknown;
            }
        }
    }
}