using JwtTokenSample.Infrastructure;
using JwtTokenSample.Models.Entities;
using JwtTokenSample.Models.Forms;
using JwtTokenSample.Models.Mappers;
using JwtTokenSample.Models.Quieries;
using JwtTokenSample.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenRepository _tokenRepository;
        

        public AuthController(IAuthRepository authRepository, ITokenRepository tokenRepository)
        {
            _authRepository = authRepository;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginForm form)
        {
            Utilisateur? utilisateur = _authRepository.Execute(new LoginQuery(form.Email, form.Passwd));

            if (utilisateur is null)
                return NotFound();

            string token = _tokenRepository.GenerateToken(utilisateur);
            return Ok(utilisateur.ToDto(token));
        }
    }
}
