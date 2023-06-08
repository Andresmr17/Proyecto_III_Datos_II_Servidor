using Xmls;
using XmlsStore;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace sqlControllers {
    [ApiController]
    [Route("sql")]
    public class sqlController : ControllerBase {
        [HttpPost]
        [Route("insert/{data}")]
        public IActionResult Register(string data) {
            Console.WriteLine(data);
            //Xml_manager.add(data);
            //JSONManager.AddToJSON<User>(newUser, "../tuto-api/DB/Entities/User.json");
            return Ok();
            //return BadRequest("La sentencia es incorrecta");
        }
        }
}
