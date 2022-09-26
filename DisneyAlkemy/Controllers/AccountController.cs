using DisneyAlkemy.Data.DataModels;
using DisneyAlkemy.DataAccess;
using DisneyAlkemy.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DisneyAlkemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DisneyContext _context;
        private readonly JwtSettings _jwtSettings;
        public AccountController(DisneyContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }
        [Route("/auth/login")]
        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var Token = new UserToken();
                var searchUser = (from user in _context.users
                                  where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                  select user).FirstOrDefault(); // busco un usuario
                if (searchUser != null)// si el user es valido
                {
                    Token = JwtHelper.GenTokenKey(new UserToken()//Creo token y le paso los datos
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid()                       
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token); // Si se logeo devuelvo el token

            }
            catch (Exception ex)
            {
                throw new Exception("Login Error", ex);

            }
            
        }


    }
}
