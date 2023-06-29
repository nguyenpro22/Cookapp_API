using Cookapp.DTOs.Account;
using Cookapp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cookapp.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller

    {
        [NonAction]
        public ObjectResult SetError(Exception e)
        {
            return StatusCode(500, e.Message);
        }
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseModel<AccountResponse> Login(string username, string password)
        {
            //
            //check user-
            //if ok return success + token
            //else return false
            //
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var tokenStr = GenerateJSONWebToken(username, password);
                Token token = new Token() { AccessToken = tokenStr };
                //string jsonRes = Newtonsoft.Json.JsonConvert.SerializeObject(token);
                AccountResponse response = new AccountResponse();
                response.phone = username;
                response.fullName = password;
                response.token = token;
                return new ResponseModel<AccountResponse> { result = response, Status = ErrorModel.SUCCESS, Message = ErrorModel.GetMessage(ErrorModel.SUCCESS) };
            }
            else
                return new ResponseModel<AccountResponse> { result = null, Status = ErrorModel.LOGIN_FAILED, Message = ErrorModel.GetMessage(ErrorModel.LOGIN_FAILED) };
        }
        private string GenerateJSONWebToken(string name, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, name),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            //
            var token = new JwtSecurityToken(
                issuer: name,
                audience: email,
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);
            //
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
