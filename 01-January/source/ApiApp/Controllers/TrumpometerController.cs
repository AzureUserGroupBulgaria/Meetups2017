using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class TrumpometerController : Controller {

        public TrumpometerController(IOptions<ConnectionStrings> options)
        {
            this.connectionStrings = options.Value;
        }

        private readonly ConnectionStrings connectionStrings;

        [HttpGet("entries")]
        public async Task<IEnumerable<TrumpometerEntry>> Entries() {

            var account = CloudStorageAccount.Parse(this.connectionStrings.StorageConnectionString);
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("entries");
            await table.CreateIfNotExistsAsync();

            var query = new TableQuery<TrumpometerEntryTableDTO>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "all"))
                .Take(10);

            var queryResult = await table.ExecuteQuerySegmentedAsync(query, null);
            var models = queryResult.Select(i => i.ToModel());

            return models;
        }

        [HttpPost("entries")]
        public async void PostEntries([FromBody]TrumpometerEntry model) {

            var account = CloudStorageAccount.Parse(this.connectionStrings.StorageConnectionString);
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("entries");
            await table.CreateIfNotExistsAsync();

            var dto = model.ToDTO();
            var operation = TableOperation.Insert(dto);
            
            await table.ExecuteAsync(operation);
        }

        [HttpPost("score")]
        public int Score([FromBody]ScoreModel model) {
 
            var scoreService = new ScoreService();

            return scoreService.GetScoreForSentence(model.Text);
        }

    }
}