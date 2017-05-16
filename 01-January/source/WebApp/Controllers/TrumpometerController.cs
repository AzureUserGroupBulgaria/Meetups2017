using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication.Configuration;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApplication.Controllers
{
    public class TrumpometerController : Controller
    {
        private readonly ApiEndpointsSettings apiEndpoints;

        public TrumpometerController(IOptions<ApiEndpointsSettings> options)
        {
            this.apiEndpoints = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(this.apiEndpoints.TrumpometerEntries);
            var responseContent = await response.Content.ReadAsStringAsync();
            var entries = JsonConvert.DeserializeObject<IList<TrumpometerEntryDTO>>(responseContent);
            var models = CreateModels(entries);

            return View(models);
        }

        private static List<TrumpometerEntryModel> CreateModels(IEnumerable<TrumpometerEntryDTO> tweets)
        {
            return tweets
                .Select(t => new TrumpometerEntryModel() {
                    TrumpBadges = GetNumberOfBadges(t.Score),
                    Text = t.Text,
                    OriginalUrl = t.OriginalUrl
                })
                .ToList();
        }

        private static int GetNumberOfBadges(int trumpScore)
        {
            if(trumpScore <= 0)
            {
                return 0;
            }
            else if(trumpScore <= 30)
            {
                return 1;
            }
            else if (trumpScore <= 50)
            {
                return 2;
            }
            else if (trumpScore <= 60)
            {
                return 3;
            }
            else if (trumpScore <= 100)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
    }
}
