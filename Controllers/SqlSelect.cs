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
            }

            return Ok(nombres);
        }

        [HttpGet]
        [Route("getStoreinfo/{data}/{nombres}")]
        public ActionResult<string[,]> GetStoreDetails(string data, string nombres)
        {
            //Obtiene el texto del cliente sin espacios
            string[] partes_data = data.Split(" ");

            //Obtiene el tamaño de esta lista
            int tamaño = partes_data.Length;

            //Obtiene el nombre de la tabla donde debe obtener los datos
            string nombre_tabla = partes_data[3];

            //Obtiene los atributos a los que se les debe obtener la información sin ","
            //[id,nombre]
            string[] atributos = nombres.Split(",");

            int tamaño_atri = atributos.Length;

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

            string[,] matriz = new string[tamaño_atri, count];

            int j = 0;

            foreach (XmlNode storeNode in nodes)
            {
                foreach (XmlNode storeAtributo in storeNode)
                {
                    for (int i = 0; i<atributos.Length; i++){
                        if (atributos[i] == storeAtributo.Name){
                            Console.WriteLine("este es el valor de i: " +i);
                            Console.WriteLine(storeAtributo.InnerText);
                            Console.WriteLine("este es el valor de j: " +j);
                            matriz[i, j] = storeAtributo.InnerText;
                            break;
                        }
                    }
                }
                j++; 
                
            } 
            /*for (int a = 0; a < tamaño_atri; a++)
            {
                for (int b = 0; b < count; b++)
                {
                    Console.Write(matriz[a, b]);
                }
            }*/

            return Ok(matriz);
            
        }
    }
}