
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        [HttpPost]
        [Route("register/{name}/{username}")]
        public IActionResult Register(string name, string username) {
            User newUser = new User() {
                name = name,
                username = username
            };
            //JSONManager.AddToJSON<User>(newUser, "../tuto-api/DB/Entities/User.json");
            //return Ok();
            return BadRequest("La sentencia es incorrecta");
        }
    }
}