using Xmls;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        [HttpPost]
        [Route("register/{username}/{email}/{password}")]
        public IActionResult Register(string username, string email, string password) {
            User newUser = new User() {
                username = username,
                email = email,
                password = password
            };
            Xml_manager.creates(username, email, password);
            //JSONManager.AddToJSON<User>(newUser, "../tuto-api/DB/Entities/User.json");
            return Ok();
            //return BadRequest("La sentencia es incorrecta");
        }
    }
}