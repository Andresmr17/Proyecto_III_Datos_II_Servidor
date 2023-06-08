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
        public IActionResult insertSql(string data) {
            //INSERT INTO ESTUDIANTE (carne,nombre,apellido) VALUES (2020129522,Andres,Molina)
            //DELETE FROM ESTUDIANTE
            //DELETE FROM ESTUDIANTE WHERE nombre=andres 

            //Hace una lista de strings que se separan cuando hay un espacio " "
            string[] partes_data = data.Split(" ");

            //Selecciona el nombre de la tabla
            string nombre_tabla = partes_data[2];

            //Selecciona los atributos que se van a agregar quitandole los parentesis
            //carne,nombre,apellido
            string atributos = partes_data[3].Substring(1,partes_data[3].Length -2 );

            //Hace una lista de string usando los atributos
            //[carne,nombre,apellido]
            string[] lista_atributos = atributos.Split(",");

            //Selecciona la informacion de los atributos quitandole los parentesis
            //2020129522,Andres,Molina
            string info_atributos = partes_data[5].Substring(1,partes_data[5].Length -2 );

            //Hace una lista de string usando los atributos
            //[2020129522,Andres,Molina]
            string[] lista_info_atributos = info_atributos.Split(",");

            
            string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            for(int i = 0; i < lista_info_atributos.Length; i++){
                XmlNodeList storeElements = xmlDoc.SelectNodes($"//Store[Nombre_Store='{nombre_tabla}']/Atributos/"+lista_atributos[i]);
               foreach (XmlElement attributeElement in storeElements)
            {
                attributeElement.InnerText = lista_info_atributos[i];
                Console.WriteLine(lista_info_atributos[i]);
            }
            }
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml"); 

        
            //return Ok(attributes);
            


            Console.WriteLine(lista_atributos[0]);
            Console.WriteLine(lista_atributos[1]);
            Console.WriteLine(lista_atributos[2]);
            Console.WriteLine(lista_info_atributos.Length);
            Console.WriteLine(lista_info_atributos[0]);
            Console.WriteLine(lista_info_atributos[1]);
            Console.WriteLine(lista_info_atributos[2]);
            //Xml_manager.add(data);
            //JSONManager.AddToJSON<User>(newUser, "../tuto-api/DB/Entities/User.json");
            return Ok();
            //return BadRequest("La sentencia es incorrecta");
        }

        [HttpPost]
        [Route("delete/{data}")]
        public IActionResult deletetSql(string data) {

            //Hay dos casos:
                //DELETE FROM ESTUDIANTE
                //DELETE FROM ESTUDIANTE WHERE nombre=andres ...

            //Hace una lista de strings que se separan cuando hay un espacio " "
            string[] partes_data = data.Split(" ");

            int tamaño = partes_data.Length;

            if (tamaño == 3){

                //Selecciona el nombre de la tabla que se va a eliminar
                string nombre_tabla = partes_data[2];

                //Se trae el archivo XML
                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNodeList storeElements = xmlDoc.SelectNodes($"//Store[Nombre_Store='{nombre_tabla}']");

                foreach (XmlNode storeNode in storeElements)
                {
                    XmlNode parentNode = storeNode.ParentNode;
                    parentNode.RemoveChild(storeNode);
                }

                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");

                return Ok();
            }

            return Ok();
        }
        }
}
