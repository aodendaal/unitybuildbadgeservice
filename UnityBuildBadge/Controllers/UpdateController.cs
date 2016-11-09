using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace UnityBuildBadge.Controllers
{
    public class UpdateController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Post(Models.BuildInfo buildInfo)
        {
            var url = "https://unitybadges.documents.azure.com:443/";
            var key = "ayrfZgmGkAuKf1NDubwnEoStE0RWjwqxfxwLG1UkAkAe41tufDDOHeztpCnwfKwmnRL6t3Y3Asmbhtxxrzu0fg==";

            var client = new DocumentClient(new Uri(url), key);

            var database = "UnityBadges";
            var collection = "Messages";

            buildInfo.ReceivedDate = DateTime.Now;

            await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(database, collection), buildInfo);

            return Ok();
        }
    }
}
