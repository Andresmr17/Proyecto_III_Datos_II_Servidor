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

            //Hay tres casos:
                //DELETE FROM ESTUDIANTE
                //DELETE FROM ESTUDIANTE WHERE nombre=andres
                //DELETE FROM ESTUDIANTE WHERE nombre=andres AND/OR apellido=molina

            //Hace una lista de strings que se separan cuando hay un espacio " "
            string[] partes_data = data.Split(" ");

            int tamaño = partes_data.Length;

            if (tamaño == 3){

                //Selecciona el nombre de la tabla que se va a eliminar
                string nombre_tabla = partes_data[2];

                //Se trae el archivo XML
                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+nombre_tabla+"/"+nombre_tabla+".xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                string minuscula = nombre_tabla.ToLower();

                XmlNodeList storeElements = xmlDoc.SelectNodes($"//Store/"+nombre_tabla+"/"+minuscula);

                foreach (XmlNode storeNode in storeElements)
                {
                    XmlNode parentNode = storeNode.ParentNode;
                    parentNode.RemoveChild(storeNode);
                }

                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+nombre_tabla+"/"+nombre_tabla+".xml");

                return Ok();
            }

            else if (tamaño == 5)
            {
                // Selecciona el nombre de la tabla que se va a eliminar
                string nombre_tabla = partes_data[2];

                // Se obtiene lo que está dentro del paréntesis
                string atributos = partes_data[4].Substring(1, partes_data[4].Length - 2);

                // Se separa cuando vea el igual, es decir, queda [atributo,condicion]
                string[] lista_atributos = atributos.Split("=");

                // Se obtiene el atributo
                string atributo = lista_atributos[0];

                // Se obtiene la condicion
                string condicion = lista_atributos[1];

                // Se trae el archivo XML
                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                // DELETE FROM PERRO WHERE (raza=pomerania)

                // Me posiciono en el nombre de la tabla en mayuscula
                XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/{nombre_tabla}");

                Console.WriteLine("Este es el atributo recibido: " + atributo);
                Console.WriteLine("Este es la condición recibida: " + condicion);

                List<XmlNode> nodesToRemove = new List<XmlNode>();

                foreach (XmlNode storeNode in storeElements)
                {

                    foreach (XmlNode storeAtributo in storeNode)
                    {
                        Console.WriteLine("Este es el atributo de comparación: " + storeAtributo.Name);
                        Console.WriteLine("Este es la condición de comparación: " + storeAtributo.InnerText);
                        if (storeAtributo.Name == atributo && storeAtributo.InnerText == condicion)
                        {
                            nodesToRemove.Add(storeNode);
                            break; // No es necesario seguir buscando en los atributos del nodo
                        }
                    }
                }

                foreach (XmlNode node in nodesToRemove)
                {
                    XmlNode parentNode = node.ParentNode;
                    parentNode.RemoveChild(node);
                }

                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml");

                return Ok();
            }
            else if (tamaño == 7)
            {
                //DELETE FROM CARRO WHERE (marca=nissa) AND (año=2020) 

                // Selecciona el nombre de la tabla que se va a eliminar
                string nombre_tabla = partes_data[2];

                // Se obtiene lo que está dentro del primer paréntesis
                string atributos = partes_data[4].Substring(1, partes_data[4].Length - 2);

                // Se separa cuando vea el igual, es decir, queda [atributo1,condicion1]
                string[] lista_atributos = atributos.Split("=");

                // Se obtiene el atributo
                string atributo = lista_atributos[0];

                // Se obtiene la condicion
                string condicion = lista_atributos[1];

                // Se obtiene el condicional
                string condicional = partes_data[5];

                // Se obtiene lo que está dentro del segundo paréntesis
                string atributos2 = partes_data[6].Substring(1, partes_data[6].Length - 2);

                // Se separa cuando vea el igual, es decir, queda [atributo2,condicion2]
                string[] lista_atributos2 = atributos2.Split("=");

                // Se obtiene el atributo
                string atributo2 = lista_atributos2[0];

                // Se obtiene la condicion
                string condicion2 = lista_atributos2[1];

                // Se trae el archivo XML
                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                // DELETE FROM PERRO WHERE (raza=pomerania)

                // Me posiciono en el nombre de la tabla en mayuscula
                XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/{nombre_tabla}");

                Console.WriteLine("Este es el primer atributo recibido: " + atributo);
                Console.WriteLine("Este es la primer condición recibida: " + condicion);
                Console.WriteLine("Este es el condicional recibido: " + condicional);
                Console.WriteLine("Este es el segundo atributo recibido: " + atributo2);
                Console.WriteLine("Este es la segunda condición recibida: " + condicion2);

                List<XmlNode> nodesToRemove = new List<XmlNode>();

                foreach (XmlNode storeNode in storeElements)
                {
                    if(condicional == "AND"){
                        foreach (XmlNode storeAtributo in storeNode)
                        {
                            Console.WriteLine("Este es el atributo de comparación: " + storeAtributo.Name);
                            Console.WriteLine("Este es la condición de comparación: " + storeAtributo.InnerText);

                            if (storeAtributo.Name == atributo && storeAtributo.InnerText == condicion)
                            {
                                foreach(XmlNode store2Atributo in storeNode){
                                    if(store2Atributo.Name == atributo2 && store2Atributo.InnerText == condicion2){
                                        nodesToRemove.Add(storeNode);
                                        break; // No es necesario seguir buscando en los atributos del nodo
                                    }
                                }
                            }
                        }
                    }
                    else if (condicional == "OR")
                    {
                            bool removeNode = false;

                            foreach (XmlNode storeAtributo in storeNode)
                            {
                                Console.WriteLine("Este es el atributo de comparación: " + storeAtributo.Name);
                                Console.WriteLine("Este es la condición de comparación: " + storeAtributo.InnerText);

                                // Verificar si se cumple al menos una de las condiciones
                                if (storeAtributo.Name == atributo && storeAtributo.InnerText == condicion)
                                {
                                    removeNode = true;
                                    break; // No es necesario seguir buscando en los atributos del nodo
                                }
                                else if (storeAtributo.Name == atributo2 && storeAtributo.InnerText == condicion2)
                                {
                                    removeNode = true;
                                    break; // No es necesario seguir buscando en los atributos del nodo
                                }
                            }

                            if (removeNode)
                            {
                                nodesToRemove.Add(storeNode);
                            }
                        }
                }
                foreach (XmlNode node in nodesToRemove)
                {
                    XmlNode parentNode = node.ParentNode;
                    parentNode.RemoveChild(node);
                }

                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml");

                return Ok();
            }
           return Ok();
        }
    }
}
