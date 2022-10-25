using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var key = "RNG";
            var password = "TestPassword";
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "QueryToDatabase")]
        public IEnumerable<WeatherForecast> GetFromDatabase([FromBody] string query)
        {
            var key = "RNG"; // hardcode key
            var password = "TestPassword"; // hardcode password

            string connectionString = "Server=.\\localhost;Database=TEST_DB;User Id=admin;Password=" + password + ";";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open(); // Never closed connection :)

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Console.WriteLine(String.Format("{0}",
            reader["id"]));
            }


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}