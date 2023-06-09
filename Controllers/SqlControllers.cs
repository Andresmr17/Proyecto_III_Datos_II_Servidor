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
            //Ejemplo de uso para su comprensión:
            //data = INSERT INTO ESTUDIANTE (carne,nombre,apellido) VALUES (2020129522,Andres,Molina) (2020129522,Andres,Molina)  
            

            //Hace una lista de strings que se separan cuando hay un espacio " "
            //[INSERT,INTO,ESTUDIANTE,(carne,nombre,apellido),VALUES,(2020129522,Andres,Molina)]
            string[] partes_data = data.Split(" ");

            int tamaño = partes_data.Length;

            //verifica si solo se quiere hacer un insert en una tabla
            if (tamaño == 6){
                //Selecciona el nombre de la tabla
                string nombre_tabla = partes_data[2];

                //Selecciona los atributos que se van a agregar quitandole los parentesis
                //carne,nombre,apellido
                string atributos = partes_data[3].Substring(1, partes_data[3].Length - 2);

                //Hace una lista de string usando los atributos
                //[carne,nombre,apellido]
                string[] lista_atributos = atributos.Split(",");

                //Selecciona la informacion de los atributos quitandole los parentesis
                //2020129522,Andres,Molina
                string info_atributos = partes_data[5].Substring(1, partes_data[5].Length - 2);

                //Hace una lista de string usando los atributos
                //[2020129522,Andres,Molina]
                string[] lista_info_atributos = info_atributos.Split(",");

                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+nombre_tabla+"/"+nombre_tabla+".xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/"+nombre_tabla);

                XmlElement nodo = xmlDoc.CreateElement(nombre_tabla.ToLower());
                storeElements.AppendChild(nodo);

                XmlAttribute atributo = storeElements.Attributes["atributo"];
                string valor_atributo = atributo.Value;
                Console.WriteLine(valor_atributo);

                string[] lista = valor_atributo.Split(",");



                for(int i = 0; i < lista_info_atributos.Length; i++){
                    
                    XmlElement elemento = xmlDoc.CreateElement(lista[i]);
                    XmlText textNode = xmlDoc.CreateTextNode(lista_info_atributos[i]);
                    elemento.AppendChild(textNode);
                    
                    nodo.AppendChild(elemento);
                    
                }

                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+nombre_tabla+"/"+nombre_tabla+".xml");

                Console.WriteLine("------------------nuevo-------------------------");
                Console.WriteLine(lista_info_atributos[0]);
                Console.WriteLine(lista_info_atributos[1]);
                Console.WriteLine(lista_info_atributos[2]);

                return Ok();
            }

            //Es para cuando se quieran hacer mas de un insert en un mismo script
            else{

                int limite = tamaño;

                for(int i = 5; i < tamaño; i++){
                    //Selecciona el nombre de la tabla
                    string nombre_tabla = partes_data[2];

                    //Selecciona los atributos que se van a agregar quitandole los parentesis
                    //carne,nombre,apellido
                    string atributos = partes_data[3].Substring(1, partes_data[3].Length - 2);

                    //Hace una lista de string usando los atributos
                    //[carne,nombre,apellido]
                    string[] lista_atributos = atributos.Split(",");

                    //Selecciona la informacion de los atributos quitandole los parentesis
                    //2020129522,Andres,Molina
                    string info_atributos = partes_data[i].Substring(1, partes_data[i].Length - 2);

                    //Hace una lista de string usando los atributos
                    //[2020129522,Andres,Molina]
                    string[] lista_info_atributos = info_atributos.Split(",");

                    string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+nombre_tabla+"/"+nombre_tabla+".xml";
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFilePath);

                    XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/"+nombre_tabla);

                    XmlElement nodo = xmlDoc.CreateElement(nombre_tabla.ToLower());
                    storeElements.AppendChild(nodo);

                    XmlAttribute atributo = storeElements.Attributes["atributo"];
                    string valor_atributo = atributo.Value;
                    Console.WriteLine(valor_atributo);

                    string[] lista = valor_atributo.Split(",");



                    for(int j = 0; j < lista_info_atributos.Length; j++){
                        
                        XmlElement elemento = xmlDoc.CreateElement(lista[j]);
                        XmlText textNode = xmlDoc.CreateTextNode(lista_info_atributos[j]);
                        elemento.AppendChild(textNode);
                        
                        nodo.AppendChild(elemento);
                        
                    }

                    xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+nombre_tabla+"/"+nombre_tabla+".xml");

                    Console.WriteLine("------------------nuevo-------------------------");
                    Console.WriteLine(lista_info_atributos[0]);
                    Console.WriteLine(lista_info_atributos[1]);
                    Console.WriteLine(lista_info_atributos[2]);
                    }
            }
            
            return Ok();
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
            else if (tamaño == 5)
            {

                string nombre_tabla = partes_data[2];
                string atributos = partes_data[3].Substring(1, partes_data[3].Length - 2);
                string[] lista_atributos = atributos.Split(",");
                string info_atributos = partes_data[5].Substring(1, partes_data[5].Length - 2);
                string[] lista_info_atributos = info_atributos.Split(",");

                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNodeList storeElements = xmlDoc.SelectNodes($"//Store[Nombre_Store='{nombre_tabla}']/Atributos");

                foreach (XmlNode storeNode in storeElements)
                {
                    // Eliminar nodos de atributos que cumplan la condición
                    var attributeNodesToRemove = storeNode.ChildNodes.Cast<XmlNode>()
                        .Where(node => lista_atributos.Contains(node.Name) && node.InnerText.Trim() == "condición")
                        .ToList();

                    foreach (var attributeNode in attributeNodesToRemove)
                    {
                        storeNode.RemoveChild(attributeNode);
                    }

                    // Eliminar nodos de atributos vacíos
                    var emptyAttributeNodes = storeNode.ChildNodes.Cast<XmlNode>()
                        .Where(node => string.IsNullOrEmpty(node.InnerText.Trim()))
                        .ToList();

                    foreach (var emptyAttributeNode in emptyAttributeNodes)
                    {
                        storeNode.RemoveChild(emptyAttributeNode);
                    }

                    for (int i = 0; i < lista_atributos.Length; i++)
                    {
                        string atributo = lista_atributos[i];
                        string valor = lista_info_atributos[i];

                        XmlElement attributeElement = xmlDoc.CreateElement(atributo);
                        XmlText textNode = xmlDoc.CreateTextNode(valor);
                        attributeElement.AppendChild(textNode);
                        storeNode.AppendChild(attributeElement);
                    }
                }

    xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");

    return Ok();
}
            
            else if (tamaño == 7)
            {
                string nombre_tabla = partes_data[2];
                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNodeList storeElements = xmlDoc.SelectNodes($"//Store[Nombre_Store='{nombre_tabla}']");

                string atributos = partes_data[4].Substring(1, partes_data[4].Length - 2);
                string[] lista_atributos = atributos.Split("=");
                string atributo = lista_atributos[0];
                string condicion = lista_atributos.Length > 1 ? lista_atributos[1] : string.Empty;

                string atributos2 = partes_data[6].Substring(1, partes_data[6].Length - 2);
                string[] lista_atributos2 = atributos2.Split("=");
                string atributo2 = lista_atributos2[0];
                string condicion2 = lista_atributos2.Length > 1 ? lista_atributos2[1] : string.Empty;

                string condicional = partes_data[5];

                foreach (XmlNode storeNode in storeElements)
                {
                    XmlNode atributosNode = storeNode.SelectSingleNode("Atributos");
                    if (atributosNode != null)
                    {
                        XmlNodeList atributoElements = atributosNode.SelectNodes("*");
                        bool cumpleCondicion = false;

                        foreach (XmlNode atributoNode in atributoElements)
                        {
                            if ((condicional == "OR" && (atributoNode.Name == atributo || atributoNode.Name == atributo2)) ||
                                (condicional == "AND" && (atributoNode.Name == atributo && atributoNode.Name == atributo2)))
                            {
                                cumpleCondicion = true;
                                break;
                            }
                        }
                        if (cumpleCondicion)
                        {
                            foreach (XmlNode atributoNode in atributoElements)
                            {
                                atributoNode.InnerText = "";
                            }
                        }
                    }
                }
                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");

                return Ok();
            }
           return Ok();
        }
    }
}
