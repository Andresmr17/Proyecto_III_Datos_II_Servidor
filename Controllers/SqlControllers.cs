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
        /// <summary>
        /// Metodo que se encarga de insertar informacion en la tabla correspondiente
        /// </summary>
        /// <param name="data"> Informacion que se usa para desarrollar la logica del INSERT </param>
        /// <returns> Retorna un valor propio de la clase ControllerBase </returns>
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
                    }
            }
            
            return Ok();
        }
        /*[HttpGet]
        [Route("select/{data}")] //este es para agarrar las tags
        //del xml store
        public IActionResult selectSql(string data) {
            string[] partes = data.Split("");
            int tamaño = partes.Length;
            if(tamaño =4){ //caso 1
                string nombre_tabla = partes[3];
                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+nombre_tabla+"/"+nombre_tabla+".xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/"+nombre_tabla);
                //la linea anterior me posiciona en las tags del xml, que diga perro
                //me posiciona en la linea perro = atributos patas raza etc
                XmlAttribute atributos = storeElements.Attributes["atributo"];
                string[atributos.Length] = atributos.Split(",");
                //agarra todos los atributos de xml store.
                return Ok(data);
                //hay variedad de casos
                //por ejemplo el inner join, and , or y asi.
                
                
            /*
                else{
                    return BadRequest();
                }*/

            /*}   
            //casos 
            /*caso 1select cant n de elementos from TABLA
            caso2: selec cant n de elementos from tabla where atributo= x
            //caso 3: select cant n de elementos from tabla where atributo 
            //and otro atributo 
            */

        /*}
        [HttpGet]
        [Route("select_texto/{data}")]
        public IActionResult selectSql(string data) {
            //aca agarro los atributos que quiero buscar
            //hay variedad de casos, evaluarlos
        }*/

            
        [HttpPost]
        [Route("delete/{data}")]
        /// <summary>
        /// Metodo encargado de realizar la logica del DELETE
        /// </summary>
        /// <param name="data"> Informacion que se usa para desarrollar la logica del DELETE </param>
        /// <returns> Retorna un valor propio de la clase ControllerBase </returns>
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

            else if (tamaño == 5) {
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

                List nodesToRemove = new List();

                foreach (XmlNode storeNode in storeElements)
                {
                    foreach (XmlNode storeAtributo in storeNode)
                    {
                    
                        if (storeAtributo.Name == atributo && storeAtributo.InnerText == condicion)
                        {
                            nodesToRemove.InsertFirst(storeNode); // Agregar a la lista personalizada
                            break; // No es necesario seguir buscando en los atributos del nodo
                        }
                    }
                }

                Node currentNode = nodesToRemove.GetFirst();

                while (currentNode != null)
                {
                    XmlNode node = (XmlNode)currentNode.GetLetter();
                    XmlNode parentNode = node.ParentNode;
                    parentNode.RemoveChild(node);
                    currentNode = currentNode.GetNext();
                }

                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml");

                return Ok();
            }
            else if (tamaño == 7)
            {
                //DELETE FROM CARRO WHERE (marca=nissan) AND (año=2020) 

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

                List nodesToRemove = new List();

                foreach (XmlNode storeNode in storeElements)
                {
                    if (condicional == "AND")
                    {
                        bool removeNode = false;

                        foreach (XmlNode storeAtributo in storeNode)
                        {

                            if (storeAtributo.Name == atributo && storeAtributo.InnerText == condicion)
                            {
                                foreach (XmlNode store2Atributo in storeNode)
                                {
                                    if (store2Atributo.Name == atributo2 && store2Atributo.InnerText == condicion2)
                                    {
                                        removeNode = true;
                                        break; // No es necesario seguir buscando en los atributos del nodo
                                    }
                                }
                            }
                        }

                        if (removeNode)
                        {
                            nodesToRemove.InsertFirst(storeNode); // Agregar a la lista personalizada
                        }
                    }
                    else if (condicional == "OR")
                    {
                        bool removeNode = false;

                        foreach (XmlNode storeAtributo in storeNode)
                        {
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
                            nodesToRemove.InsertFirst(storeNode); // Agregar a la lista personalizada
                        }
                    }
                }

                Node currentNode = nodesToRemove.GetFirst();

                while (currentNode != null)
                {
                    XmlNode node = (XmlNode)currentNode.GetLetter();
                    XmlNode parentNode = node.ParentNode;
                    parentNode.RemoveChild(node);
                    currentNode = currentNode.GetNext();
                }

                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml");

                return Ok();
            }

            return Ok();
        }

        [HttpPost]
        [Route("update/{data}")]
        /// <summary>
        /// Metodo encargado de desarrollar la logica del UPDATE
        /// </summary>
        /// <param name="data"> Informacion que se usa para desarrollar la logica del UPDATE </param>
        /// <returns> Retorna un valor propio de la clase ControllerBase </returns>
        public IActionResult updateSql(string data) {
            //Ejemplo de uso para su comprensión:
            //data = UPDATE CARRO SET color=verde WHERE marca=toyota
            string[] partes_data = data.Split(" ");

            int tamaño = partes_data.Length;

            //verifica si solo se quiere hacer un insert en una tabla
            if (tamaño == 6){
                //Selecciona el nombre de la tabla
                string nombre_tabla = partes_data[1];

                //Selecciono la condicion
                string cambio = partes_data[3];

                //Crea una lista con el =
                string[] camibo_lista = cambio.Split("=");

                //Selecciona el atributo a camiar
                //[color,verde]
                string atributo = camibo_lista[0];

                //Selecciona el valor del atributo
                //color
                string valor_atributo = camibo_lista[1];

                //Selecciono la condicion
                //verde
                string condicion = partes_data[5];

                //Crea una lista con el =
                //[marca,toyota]
                string[] condicion_lista = condicion.Split("=");

                //Selecciona el atributo a camiar
                string atributo_condicion = condicion_lista[0];

                //Selecciona el valor del atributo
                string valor_atributo_condicion = condicion_lista[1];

                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);


                // Me posiciono en el nombre de la tabla en mayuscula
                XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/{nombre_tabla}");

                foreach (XmlNode storeNode in storeElements)
                {
                    foreach (XmlNode storeAtributo in storeNode)
                    {
                        if (storeAtributo.Name == atributo_condicion && storeAtributo.InnerText == valor_atributo_condicion)
                        {
                            foreach(XmlNode Node in storeNode){
                                
                                if(Node.Name == atributo){
                                    Console.WriteLine("entra en el ultimo foreach");
                                    Node.InnerText = valor_atributo;
                                }
                            }
                        }
                    }
                } 
                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml");

                return Ok();                  
            }

            //data = UPDATE CARRO SET marca=ferrari WHERE marca=toyota AND/OR año=2020
            else  
            {
                //UPDATE CARRO SET marca=ferrari WHERE marca=toyota AND/OR año=2020

                // Selecciona el nombre de la tabla que se va a eliminar
                string nombre_tabla = partes_data[1];

                // Se obtiene lo que está dentro del primer paréntesis
                string atributos = partes_data[3];

                // Se separa cuando vea el igual, es decir, queda [marca,ferrari]
                string[] lista_atributos = atributos.Split("=");

                // Se obtiene el atributo "marca"
                string atributo = lista_atributos[0];

                // Se obtiene la condicion "ferrari"
                string condicion_cambio = lista_atributos[1];

                // Se obtiene la primera condicion
                string atributos_condicion1 = partes_data[5];

                // Se obtiene la segunda condicion
                string atributos_condicion2 = partes_data[7];

                // Se obtiene el condicional
                string condicional = partes_data[6];
                
                // Se separa cuando vea el igual, es decir, queda [marca,toyota]
                string[] lista_atributos1 = atributos_condicion1.Split("=");

                // Se obtiene el atributo
                // marca
                string atributo1 = lista_atributos1[0];

                // Se obtiene la condicion
                // toyota
                string valor_atributo1 = lista_atributos1[1];

                // Se separa cuando vea el igual, es decir, queda [año,2020]
                string[] lista_atributos2 = atributos_condicion2.Split("=");

                // Se obtiene el atributo
                // año
                string atributo2 = lista_atributos2[0];

                // Se obtiene la condicion
                // 2020
                string valor_atributo2 = lista_atributos2[1];

                // Se trae el archivo XML
                string xmlFilePath = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                // Me posiciono en el nombre de la tabla en mayuscula
                XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/{nombre_tabla}");

                foreach (XmlNode storeNode in storeElements)
                {
                    
                    
                    if(condicional == "AND"){
                        foreach (XmlNode storeAtributo in storeNode)
                        {
                            if (storeAtributo.Name == atributo1 && storeAtributo.InnerText == valor_atributo1)
                            {
                                foreach(XmlNode store2Atributo in storeNode){
                                    Console.WriteLine("entra en el ultimo foreach");
                                    if(store2Atributo.Name == atributo2 && store2Atributo.InnerText == valor_atributo2){
                                        

                                        foreach(XmlNode Node in storeNode){
                                            
                                
                                            if(Node.Name == atributo){
                                                
                                                Node.InnerText = condicion_cambio;
                                            }
                                        } 
                                    }
                                }
                            }
                        }
                    }
                    else if (condicional == "OR")
                    {
                        
                            foreach (XmlNode storeAtributo in storeNode)
                            {                             
                                   // Verificar si se cumple al menos una de las condiciones
                                    if (storeAtributo.Name == atributo1 && storeAtributo.InnerText == valor_atributo1)
                                    {
                                        foreach(XmlNode Node in storeNode){
                                            if(Node.Name == atributo){
                                                Node.InnerText = condicion_cambio;
                                            }
                                        }
                                    }
                                    else if (storeAtributo.Name == atributo2 && storeAtributo.InnerText == valor_atributo2)
                                    {
                                        foreach(XmlNode Node in storeNode){ 
                                            if(Node.Name == atributo){
                                                Node.InnerText = condicion_cambio;
                                            }
                                        }
                                    
                                    }
                            }
                    }
                }
                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/" + nombre_tabla + "/" + nombre_tabla + ".xml");

                return Ok();
            }

        }
    }
}
