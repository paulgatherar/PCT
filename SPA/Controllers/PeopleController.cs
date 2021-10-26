using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private readonly INameReverseService nameReverseService;

        public PeopleController(IHttpClientFactory httpClientFactory, INameReverseService nameReverseService)
        {
            this.httpClientFactory = httpClientFactory;
            this.nameReverseService = nameReverseService;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get([FromQuery] bool sortByAge, [FromQuery] bool reverseNames)
        {
            var httpClient = this.httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.GetAsync(url);

            // Can throw an exception. Handled by middleware.
            httpResponseMessage.EnsureSuccessStatusCode();

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(content);

            if (sortByAge)
            {
                people = people.OrderBy(p => p.Age);
            }

            if (reverseNames)
            {
                people = people.Select(p =>
                new Person
                {
                    Id = p.Id,
                    Name = this.nameReverseService.Reverse(p.Name),
                    Age = p.Age
                });
            }

            return people;
        }

        [HttpGet("download")]
        public async Task<ActionResult> Download([FromQuery] bool sortByAge, [FromQuery] bool reverseNames)
        {
            var people = await this.Get(sortByAge, reverseNames);

            var sb = new StringBuilder();

            sb.AppendLine("ID,Name,Age");

            foreach (var person in people)
            {
                sb.AppendLine($"{person.Id},{person.Name},{person.Age}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "FEFle123.csv");
        }
    }
}
