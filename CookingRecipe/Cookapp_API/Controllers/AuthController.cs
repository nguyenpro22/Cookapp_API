using Azure;
using Cookapp_API.Data;
using Cookapp_API.DataAccess.BLL;
using Cookapp_API.DataAccess.DTO.AllInOneDTO;
using Cookapp_API.DataAccess.DTO.AllInOneDTO.AccoountDTO;
using Cookapp_API.DataAccess.DTO.AllInOneDTO.AccountDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cookapp_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase

    {
        [NonAction]
        public ObjectResult SetError(Exception e)
        {
            return StatusCode(500, e.Message);
        }
        private IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseModel<LoginResponse> Login(string username, string password)
        {
            
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var tokenStr = GenerateJSONWebToken(username, password);
                Cookapp_API.DataAccess.DTO.AllInOneDTO.AccountDTO.Token token = new Cookapp_API.DataAccess.DTO.AllInOneDTO.AccountDTO.Token() { AccessToken = tokenStr };
                //string jsonRes = Newtonsoft.Json.JsonConvert.SerializeObject(token);
                
                //
                AccountBLL bll = new AccountBLL(_config["ConnectionStrings:CookappDB"], DataAccess.ESqlProvider.SQLSERVER, 120);
                LoginResponse account =  bll.isAuthenticated(username, password);
                account.username = username;
                account.Password = password;
                account.token = token;
                //abc

                
                ResponseModel<LoginResponse> res = new ResponseModel<LoginResponse>();
                if (account !=null) { 
                    res.Success = true;
                    res.Result = account;
                    res.ResponseMessage = "OK";
                    res.ResponseCode = 200;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Result = account;
                    res.ResponseMessage = "Incorrect Username or Password";
                    res.ResponseCode = 401;
                    return res;
                }
                    //return new ActionResult<LoginResponse> { result = null, Status = ErrorModel.LOGIN_FAILED, Message = ErrorModel.GetMessage(ErrorModel.LOGIN_FAILED) };
            }
            else
            {
                ResponseModel<LoginResponse> res = new ResponseModel<LoginResponse>();
                res.Success = true;
                res.Result = null;
                res.ResponseMessage = "Username, pass is requied";
                res.ResponseCode = 405;
                return res;
            }
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

