using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using StudiTrain.Models;
using StudiTrain.Models.Database;
using StudiTrain.Setup;

namespace StudiTrain.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : StudiTrainController
    {
        public UsersController(IAppSettings settings) : base(settings) { }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate([FromBody] AuthModel userInput)
        {
            var user = Services.UserService.Authenticate(userInput);
            if (!user.Authenticated)
                return Unauthorized();
            return user.GetJwToken();
        }

        [AllowAnonymous]
        [HttpPost("authenticate/postman")]
        public ActionResult AuthenticateForPostman([FromBody] AuthModel userInput)
        {
            var user = Services.UserService.Authenticate(userInput);
            if (!user.Authenticated)
                return Unauthorized();
            return Ok(new Dictionary<string,string>
            {
                {"access_token", user.GetJwToken() },
                {"expires_in", user.JwToken.ValidTo.ToString(CultureInfo.CurrentCulture)}
            });
        }
    }
}
