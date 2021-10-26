using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SPA.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        const string url = "https://techtestpersonapi.azurewebsites.net/api/GETPersonsTechTestAPI?code=Z5Dm297Ijn9weSo75EVtsJHN9HoVE0fgJt8zIGXWV4ZOOCGNpaYBtw==";

        private readonly IHttpClientFactory httpClientFactory;

        public PeopleController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var httpClient = this.httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.GetAsync(url);

            // Can throw an exception. Handled by middleware.
            httpResponseMessage.EnsureSuccessStatusCode();

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<Person>>(content);
        }
    }
}
