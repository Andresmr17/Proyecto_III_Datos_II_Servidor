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

        /// <summary>
        /// Metodo que llama al metodo encargado de crear el XML store con la informacion solicitada
        /// </summary>
        /// <param name="storename"> Nombre del XML store </param>
        /// <param name="atributos"> Atributos del XML store </param>
        /// <returns> Retorna un valor propio de la clase ControllerBase </returns>
        public IActionResult Creacion(string storename, string atributos)
        {
            XmlStoreManager.creates(storename, atributos);
            return Ok();
        }

        [HttpGet]
        [Route("getStoreNames")]

        /// <summary>
        /// Metodo que obtiene el nombre de los XML stores creados
        /// </summary>
        /// <param> No tiene parametros </param>
        /// <returns> Retorna el nombre de los XML stores creados </returns>
        public IActionResult GetStoreNames()
        {
            string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores";
            string[] carpetas = Directory.GetDirectories(xmlFilePath);
            

            List<string> storeNames = new List<string>();
            foreach (string carpeta in carpetas)
            {
                string nombreCarpeta = Path.GetFileName(carpeta);
                storeNames.Add(nombreCarpeta);
                Console.WriteLine(nombreCarpeta);
            }

            return Ok(storeNames);
        }
        
        [HttpGet]
        [Route("getStoreDetails/{storeName}")]

        /// <summary>
        /// Metodo que obtiene los atributos de los XML store creados
        /// </summary>
        /// <param name="storeName"> Nombre del XML store del que se desea obtener la informacion </param>
        /// <returns> Retorna la informacion del XML store creado </returns>
        public IActionResult GetStoreDetails(string storeName)
        {
            string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+storeName+"/"+storeName+".xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            string minuscula = storeName.ToLower();

            
            XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/"+storeName);
            XmlAttribute atributo = storeElements.Attributes["atributo"];
            string valor_atributo = atributo.Value;
            

            string[] lista = valor_atributo.Split(",");
            

            return Ok(lista);
        }
    }
}