using Xmls;
using XmlsStore;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Xml;

namespace Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        [HttpPost]
        [Route("register/{username}/{email}/{password}")]
        public IActionResult Register(string username, string email, string password) {
            
            Xml_manager.add(username, email, password);
            //JSONManager.AddToJSON<User>(newUser, "../tuto-api/DB/Entities/User.json");
            return Ok();
            //return BadRequest("La sentencia es incorrecta");
        }

        [HttpGet]
        [Route("login/{username}/{password}")]
        public IActionResult login(string username, string password) {
            
            if(Xml_manager.verify(username ,password)){

                var data = new {JsonResult = "Hola esta informacion se manda desde el backend"};

                return Ok(data);
            }
            else{
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("creacion/{storename}/{atributos}")]
        public IActionResult creacion(string storename, string atributos) {
            
            XmlStore_manager.add(storename, atributos);
            //JSONManager.AddToJSON<User>(newUser, "../tuto-api/DB/Entities/User.json");
            return Ok();
            //return BadRequest("La sentencia es incorrecta");
        }

        [HttpGet]
        [Route("getStoreNames")]
        public IActionResult GetStoreNames()
        {
            string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            
            List<string> storeNames = new List<string>();
            XmlNodeList storeElements = xmlDoc.SelectNodes("//Store");
            foreach (XmlElement storeElement in storeElements)
            {
                string storeName = storeElement.InnerText;
                storeNames.Add(storeName);
                }
                
                return Ok(storeNames);
        }
    }
}