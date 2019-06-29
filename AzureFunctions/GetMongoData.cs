using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.MyMedicine;
using Microsoft.Net.Http.Headers;

namespace MyMedicine.Functions
{
    public static class GetMongoData
    {
        [FunctionName("GetMongoData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string itemType = req.Query["type"];
            string name = req.Query["name"];
            string updatedAt = req.Query["updatedAt"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            itemType = itemType ?? data?.type;
            updatedAt = updatedAt ?? data?.updatedAt;

            var etag = $"{itemType}-{name}";
            var lastModified = DateTime.Now;
            if (updatedAt != null) {
                lastModified = Convert.ToDateTime(updatedAt);
            }
            var requestHeaders = req.GetTypedHeaders();

            // if (requestHeaders.IfNoneMatch != null && requestHeaders.IfNoneMatch.Contains(new EntityTagHeaderValue(etag))) {
            //     return new NotModifiedResult(lastModified, etag);
            // }

            dynamic repo = Repository<User>.Instance;
            switch (itemType)
            {
                case nameof(Medicine):
                    repo = Repository<Medicine>.Instance;
                    break;
                default:
                    break;
            }
            
            var items = await (string.IsNullOrWhiteSpace(name) ? repo.FindMany(itemType) : repo.FindMany(itemType, name));
 
            return items != null
                ? (ActionResult)new OkObjectResult(items)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
