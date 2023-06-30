using JwtTokenSample.Infrastructure;
using JwtTokenSample.Models.Dtos;
using JwtTokenSample.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITokenRepository _tokenRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITokenRepository tokenRepository)
        {
            _logger = logger;
            _tokenRepository = tokenRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastDto> Get()
        {
            //Recupération du Token
            string token = Request.Headers.Authorization.Single(a => a.StartsWith("Bearer "))!;
            UtilisateurDto utilisateur = _tokenRepository.GetUtilisateur(token);

            //int id = int.Parse(User.Claims.SingleOrDefault(c => c.Type == "Id")?.Value ?? "0");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}