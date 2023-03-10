using AutoMapper;
using CustomerProducts.API.JWT.Models;
using CustomerProducts.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomerProducts.API.JWT.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public AuthenticationController(IConfiguration configuration,IUserRepository userRepository, IMapper mapper)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper)); ;
        }

        public class ValidationRequestBody { 
            public string UserName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
         
        }

        [HttpPost("validate")]
        public ActionResult<string> Validate(ValidationRequestBody validationRequestBody) {

            var user =ValidateUserCredentials(validationRequestBody.UserName, validationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimForToken = new List<Claim>();
            claimForToken.Add(new Claim("sub", user.UserName));
            claimForToken.Add(new Claim("given_name", user.FirstName));
            claimForToken.Add(new Claim("family_name", user.LastName));


            var jwtToken = new JwtSecurityToken(
                   _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claimForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials
                );

            var token = new JwtSecurityTokenHandler()
                              .WriteToken(jwtToken);

            return Ok(token);
        }

        private UserDto ValidateUserCredentials(string userName, string password)
        {
            var userEntity = _userRepository.Validate(userName, password);

           return _mapper.Map<UserDto>(userEntity);
        }
    }
}
