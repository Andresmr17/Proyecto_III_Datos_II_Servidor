using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace selectControllers
{
    [ApiController]
    [Route("sqls")]
    public class selectController : ControllerBase
    {

        public string[] Atributos { get; set; }
        public string[][] Contenido { get; set; } 
        
        [HttpGet]
        [Route("select/{tabla}")]
        public IActionResult GetStoreNames(string tabla)
        {
            //Ejemplo para la comprensión:
            //SELECT (id,carné) FROM ESTUDIANTE

            //Hace una lista con la instrucción del Cliente
            //[SELECT,(id,carné),FROM,ESTUDIANTE]
            string[] partes_data = tabla.Split(" ");

            //Obtiene el tamaño de esta lista
            int tamaño = partes_data.Length;
            
                //Obtiene el nombre de la tabal de la cual hay que seleccioanr la información
            //ESTUDIANTE
            string nombre_tabla = partes_data[3];

            //Obtiene los atributos a trabajar, elimina los paréntesis
            //id,carné
            string alias = partes_data[1].Substring(1, partes_data[1].Length - 2);

            //Particiona el texto donde hay coma y lo ingresa en una lista
            //[id,carné]
            string[] lista_atributos = alias.Split(",");

            //Imprime el contenido de esta lista
            for (int i = 0; i < lista_atributos.Length; i++)
            {
                Console.WriteLine("Atributos solicitados: " + lista_atributos[i]);
            }

            //Cargo el documento con el nombre deseado por el usuario
            string xmlFilePath = $"../Proyecto_III_Datos_II_Servidor/Xmls/Stores/{nombre_tabla}/{nombre_tabla}.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            //Obtiene el nombre de la tabla en minuscula
            //estudiante
            string minuscula = nombre_tabla.ToLower();

            XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/{nombre_tabla}");

            //Obtiene los aributos de la tabla Estudiante
            //atributo="id,carné,nombre,apellido,edad"
            XmlAttribute atributo = storeElements.Attributes["atributo"];
            string valor_atributo = atributo.Value;
            Console.WriteLine("Atributos en el Xml Store: " + valor_atributo);

            string[] lista = valor_atributo.Split(",");

            List<string> nombres = new List<string>();

            foreach (string atributoCliente in lista_atributos)
            {
                if (lista.Contains(atributoCliente))
                {
                    //nombres son lo que retorna esta función, es decir, los atributos que envia el cliente que están en el Xml Store
                    nombres.Add(atributoCliente);
                }
            }return Ok(nombres);
        
        }
//aca abajo es donde debo trajar para la data con condicionales.
        [HttpGet]
        [Route("getStoreinfo/{data}/{nombres}")]
        public ActionResult<string[,]> GetStoreDetails(string data, string nombres)
        {
            //Obtiene el texto del cliente sin espacios
            string[] partes_data = data.Split(" "); //toda la info
            //tipo SELECT FROM X, WHERE...
            
            //Obtiene el tamaño de esta lista
            int tamaño = partes_data.Length;
            Console.WriteLine("el tamaño es"+tamaño);

            //Obtiene el nombre de la tabla donde debe obtener los datos
            string nombre_tabla = partes_data[3];

            //Obtiene los atributos a los que se les debe obtener la información sin ","
            //[id,nombre]
            string[] atributos = nombres.Split(",");

            int tamaño_atri = atributos.Length;
            List<List<string>> matriz = new List<List<string>>();
            if (tamaño==4){
                //Obtiene los atributos que se deben comparar con los del Xml Store 
                for (int i = 0; i < atributos.Length; i++){
                    Console.WriteLine(atributos[i]);
                }

                int count = 0;
                string xmlFilePath = $"../Proyecto_III_Datos_II_Servidor/Xmls/Stores/{nombre_tabla}/{nombre_tabla}.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                string minuscula = nombre_tabla.ToLower();

                XmlNodeList nodes = xmlDoc.SelectNodes($"//Store/{nombre_tabla}/{minuscula}");
            
                List<string> contenido = new List<string>();

                foreach(XmlNode store in nodes){
                    count++;
                }

            //string[,] matriz = new string[tamaño_atri, count];
            // ...

                foreach (XmlNode storeNode in nodes)
                {
                    List<string> fila = new List<string>();

                    foreach (XmlNode storeAtributo in storeNode)
                    {
                        for (int i = 0; i < atributos.Length; i++)
                        {
                            if (atributos[i] == storeAtributo.Name)
                            {
                                fila.Add(storeAtributo.InnerText);
                                break;
                            }
                        }
                    }
                    matriz.Add(fila);
                }
            }
            if (tamaño==6){
                Console.WriteLine("entre aca");
                string condiciones = partes_data[5].Substring(1, partes_data[5].Length - 2);
                //esto me retorna la 1 condicion despues del where.
                string[] lista_condiciones = condiciones.Split("=");
                //me da una lista de condiciones
                for (int i = 0; i < atributos.Length; i++){
                    Console.WriteLine(atributos[i]);
                }

                int count = 0;
                string xmlFilePath = $"../Proyecto_III_Datos_II_Servidor/Xmls/Stores/{nombre_tabla}/{nombre_tabla}.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                string minuscula = nombre_tabla.ToLower();
                XmlNodeList nodes = xmlDoc.SelectNodes($"//Store/{nombre_tabla}/{minuscula}");
            
                List<string> contenido = new List<string>();

                foreach(XmlNode store in nodes){
                    count++;
                }

            //string[,] matriz = new string[tamaño_atri, count];
            // ...
            // para compara si es
                //igual a la condicional.
                
                foreach (XmlNode storeNode in nodes){//este for each es para cada tabla de perro
                    List<string> fila = new List<string>();
                    bool flag=false;
                    List<string> variables;
                    foreach (XmlNode storeAtributo in storeNode){//este for each es para las variables 
                    //de una de las tablas registradas, que diga tabla perro 1
                    //agarreme lo que tenga
                        variables = new List<string>();
                        //esto probablemente sea ineficiente AF
                        //pero no se me ocurre otra manera de verificar 
                        //la condicional, sin modificar mucho este codigo.
                        for (int i = 0; i < atributos.Length; i++){
                            if (atributos[i] == storeAtributo.Name )
                            {
                                variables.Add(storeAtributo.InnerText);
                                if(atributos[i]==lista_condiciones[0] &&storeAtributo.InnerText==lista_condiciones[1]){
                                    flag=true;
                                }
                                //fila.Add(storeAtributo.InnerText);
                               // break; //este break esta bien aqui?
                            }
                        }
                        if(flag==true){
                            //si encontro el valor que coincide 
                            //lo añade a la lista, hace break y lo añade a la matriz
                            //flag=false;
                            int valor=variables.Count; //tamaño de la lista valores
                            for (int i = 0; i < valor; i++){
                                fila.Add(variables[i]);
                                Console.WriteLine("las variables con filtro "+ variables[i]);
                            }
                            //break; //este break deberia de estar?
                        }//deberia de ir otro break aca abajo?

                    }
                    matriz.Add(fila);
                }
                //return Ok(matriz); //retorno global
            }
            if(tamaño==8){
                Console.WriteLine("entre aca");
                string condiciones = partes_data[5].Substring(1, partes_data[5].Length - 2);
                string condiciones2 = partes_data[7].Substring(1, partes_data[7].Length - 2);
                string operador = partes_data[6];
                //esto me retorna la 1 condicion despues del where.
                string[] lista_condiciones = condiciones.Split("=");
                string[] lista_condiciones2 = condiciones.Split("=");
                //me da una lista de condiciones
                for (int i = 0; i < atributos.Length; i++){
                    Console.WriteLine(atributos[i]);
                }
                int count = 0;
                string xmlFilePath = $"../Proyecto_III_Datos_II_Servidor/Xmls/Stores/{nombre_tabla}/{nombre_tabla}.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                string minuscula = nombre_tabla.ToLower();
                XmlNodeList nodes = xmlDoc.SelectNodes($"//Store/{nombre_tabla}/{minuscula}");
            
                List<string> contenido = new List<string>();

                foreach(XmlNode store in nodes){
                    count++;
                }
                foreach (XmlNode storeNode in nodes){//este for each es para cada tabla de perro
                    List<string> fila = new List<string>();
                    int flag=0;
                    List<string> variables;
                    
                    foreach (XmlNode storeAtributo in storeNode){//este for each es para las variables 
                    //de una de las tablas registradas, que diga tabla perro 1
                    //agarreme lo que tenga
                        variables = new List<string>();
                        //esto probablemente sea ineficiente AF
                        //pero no se me ocurre otra manera de verificar 
                        //la condicional, sin modificar mucho este codigo.
                        for (int i = 0; i < atributos.Length; i++){
                            if (atributos[i] == storeAtributo.Name )
                            {
                                variables.Add(storeAtributo.InnerText);
                                if(atributos[i]==lista_condiciones[0] &&storeAtributo.InnerText==lista_condiciones[1]){
                                    flag+=1;
                                }
                                if(atributos[i]==lista_condiciones[0] &&storeAtributo.InnerText==lista_condiciones2[1]){
                                    flag+=1;
                                }
                                //fila.Add(storeAtributo.InnerText);
                               // break; //este break esta bien aqui?
                            }
                        }
                        if(flag==2 && operador =="AND"){
                            //flag=0;
                            //si encontro el valor que coincide 
                            //lo añade a la lista, hace break y lo añade a la matriz
                            //flag=false;
                            int valor=variables.Count; //tamaño de la lista valores
                            for (int i = 0; i < valor; i++){
                                fila.Add(variables[i]);
                                Console.WriteLine("las variables con filtro "+ variables[i]);
                            }
                            //break; //este break deberia de estar?
                        }//deberia de ir otro break aca abajo?
                        if(flag>=0 && operador =="OR"){
                            //flag=0;
                            //si encontro el valor que coincide 
                            //lo añade a la lista, hace break y lo añade a la matriz
                            //flag=false;
                            int valor=variables.Count; //tamaño de la lista valores
                            for (int i = 0; i < valor; i++){
                                fila.Add(variables[i]);
                                Console.WriteLine("las variables con filtro "+ variables[i]);
                            }
                            //break; //este break deberia de estar?
                        }//deberia de ir otro break aca abajo?

                    }
                    matriz.Add(fila);
                }
                
            }
            //////////////////// 2 brakets despues de este ok
            return Ok(matriz); 
        }
    }
}