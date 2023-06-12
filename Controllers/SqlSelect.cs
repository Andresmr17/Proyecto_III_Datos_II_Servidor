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
            string[] partes_data = tabla.Split(" ");
            int tamaño = partes_data.Length;
            string nombre_tabla = partes_data[3];
            string alias = partes_data[1].Substring(1, partes_data[1].Length - 2);
            string[] lista_atributos = alias.Split(",");

            for (int i = 0; i < lista_atributos.Length; i++)
            {
                Console.WriteLine("Atributos solicitados: " + lista_atributos[i]);
            }

            string xmlFilePath = $"../Proyecto_III_Datos_II_Servidor/Xmls/Stores/{nombre_tabla}/{nombre_tabla}.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            string minuscula = nombre_tabla.ToLower();

            XmlNode storeElements = xmlDoc.SelectSingleNode($"//Store/{nombre_tabla}");

            XmlAttribute atributo = storeElements.Attributes["atributo"];
            string valor_atributo = atributo.Value;
            Console.WriteLine("Atributos en el Xml Store: " + valor_atributo);

            string[] lista = valor_atributo.Split(",");

            List<string> nombres = new List<string>();

            foreach (string atributoCliente in lista_atributos)
            {
                if (lista.Contains(atributoCliente))
                {
                    nombres.Add(atributoCliente);
                }
            }

            return Ok(nombres);
        }

        [HttpGet]
        [Route("getStoreinfo/{tabla}/{nombres}")]
        public IActionResult GetStoreDetails(string tabla, string nombres)
        {
            string[] partes_data = tabla.Split(" ");
            int tamaño = partes_data.Length;
            string nombre_tabla = partes_data[3];
            string alias = partes_data[1].Substring(1, partes_data[1].Length - 2);
            string[] lista_atributos = alias.Split(",");

            string xmlFilePath = $"../Proyecto_III_Datos_II_Servidor/Xmls/Stores/{nombre_tabla}/{nombre_tabla}.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNodeList nodes = xmlDoc.SelectNodes($"//{nombre_tabla}");

            List<string> contenido = new List<string>();
            foreach (XmlNode storeNode in nodes)
            {

                foreach (XmlNode storeAtributo in storeNode)
                {
                    contenido.Add(storeNode.InnerText);
                    Console.WriteLine(storeNode.InnerText);
                }
            }
            return Ok(contenido);
        }
    }
}