namespace DocumentDBSample
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BacklogItem
    {
        public BacklogItem(
            string id,
            string summary,
            string category)
        {
            this.Id = id;
            this.Category = category;
            this.Summary = summary;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Summary { get; set; }

        public string Category { get; set; }

        public void ChangeCategory(string category)
        {
            this.Category = category;
        }

        public void Summarize(string summary)
        {
            this.Summary = summary;
        }
    }
}
