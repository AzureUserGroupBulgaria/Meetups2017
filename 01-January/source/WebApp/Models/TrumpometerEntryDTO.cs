using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Models
{
    public class TrumpometerEntryDTO
    {
        public string OriginalUrl { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public string Id { get; set; }
    }
}
