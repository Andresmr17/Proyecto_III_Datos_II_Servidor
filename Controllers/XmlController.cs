using XmlsStore;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace xmlControllers {
    [ApiController]
    [Route("xml")]
    public class UserController : ControllerBase {

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
                XmlNode storeNameNode = storeElement.SelectSingleNode("Nombre_Store");
                string storeName = storeNameNode.InnerText;
                storeNames.Add(storeName);
            }

            return Ok(storeNames);
        }

        [HttpGet]
        [Route("getStoreDetails/{storeName}")]
        public IActionResult GetStoreDetails(string storeName)
        {
            string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            List<string> attributes = new List<string>();
            XmlNodeList storeElements = xmlDoc.SelectNodes($"//Store[Nombre_Store='{storeName}']/Atributos/*");
            foreach (XmlElement attributeElement in storeElements)
            {
                string attributeName = attributeElement.Name;
                attributes.Add(attributeName);
            }

            return Ok(attributes);
        }
    }
}