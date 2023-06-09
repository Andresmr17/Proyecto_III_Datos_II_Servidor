using System.Collections.Generic;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace XmlsStore
{
    [ApiController]
    [Route("xml")]
    public class XmlController : ControllerBase
    {
        [HttpPost]
        [Route("creacion/{storename}/{atributos}")]
        public IActionResult Creacion(string storename, string atributos)
        {
            XmlStoreManager.add(storename, atributos);
            return Ok();
        }

        [HttpGet]
        [Route("getStoreNames")]
        public IActionResult GetStoreNames()
        {
            string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            List<string> storeNames = new List<string>();
            XmlNodeList storeElements = xmlDoc.SelectNodes("//Store/*/*");
            foreach (XmlElement storeElement in storeElements)
            {
                string storeName = storeElement.Name;
                if (!storeNames.Contains(storeName))
                {
                    storeNames.Add(storeName);
                }
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
            XmlNodeList storeElements = xmlDoc.SelectNodes($"//Store/{storeName}S/{storeName}/*");
            foreach (XmlElement attributeElement in storeElements)
            {
                string attributeName = attributeElement.Name;
                attributes.Add(attributeName);
            }

            return Ok(attributes);
        }
    }
}