using System.Xml;

namespace XmlsStore
{
    /// <summary>
    /// Clase que contiene los metodos encargados de crear los XML Store
    /// </summary>
    public class XmlStoreManager
    {

        /// <summary>
        /// Metodo que crea un documento XMl con el nombre y los atributos deseados
        /// </summary>
        /// <param name="storename"> Nombre del XML store </param>
        /// <param name="atributos"> Atributos del XML store </param>
        /// /// <returns> No retorna nada </returns>
        public static void creates(string storename, string atributos)
        {
            //../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml

            string rutaCarpeta = "../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+storename;
            if (!Directory.Exists(rutaCarpeta))
            {
                // Crear la carpeta
                Directory.CreateDirectory(rutaCarpeta);
                Console.WriteLine("Carpeta creada con éxito.");

                // Crea un documento XML
                XmlDocument xmlDoc = new XmlDocument();

                // Crea el elemento raíz "Stores"
                XmlElement rootElement = xmlDoc.CreateElement("Stores");
                xmlDoc.AppendChild(rootElement);

                // Crea un elemento "Store"
                XmlElement storeElement = xmlDoc.CreateElement("Store");
                rootElement.AppendChild(storeElement);

                // Añade el elemento storename + "S"
                XmlElement storeNamePluralElement = xmlDoc.CreateElement(storename);
                storeElement.AppendChild(storeNamePluralElement);

                // Añade el elemento storename
                

                XmlAttribute atributo = xmlDoc.CreateAttribute("atributo");
                atributo.Value = atributos;
                storeNamePluralElement.Attributes.Append(atributo);

                // Guarda el documento XML en un archivo
                xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/"+storename+"/"+storename+".xml");
            }
            else
            {
                Console.WriteLine("La carpeta ya existe.");
            }
        }

        /// <summary>
        /// Metodo que inserta la informacion de los XML store
        /// </summary>
        /// <param name="storename"> Nombre del XML store </param>
        /// <param name="atributos"> Atributos del XML store </param>
        /// /// <returns> No retorna nada </returns>
        public static void add(string storename, string atributos)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");

            // Obtiene el elemento raíz "Stores"
            XmlElement rootElement = xmlDoc.DocumentElement;

            // Busca el elemento storename + "S"
            XmlElement storeNamePluralElement = rootElement.SelectSingleNode($"Store/{storename}") as XmlElement;
            

            if (storeNamePluralElement == null)
            {
                // Si no existe el elemento storename + "S", crea uno nuevo
                storeNamePluralElement = xmlDoc.CreateElement(storename);
                rootElement.SelectSingleNode("Store").AppendChild(storeNamePluralElement);
            }

            XmlAttribute atributo = xmlDoc.CreateAttribute("atributo");
            atributo.Value = atributos;
            storeNamePluralElement.Attributes.Append(atributo);

            // Guarda los cambios en el documento XML
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");
        }

        /// <summary>
        /// Metodo que verifica si un XML store existe
        /// </summary>
        /// <param name="name"></param>
        /// <param name="atributo"></param>
        /// <returns> Retorna un valor booleano </returns>
        public static bool verify(string name, string atributo)
        {
            // Carga el documento XML existente de usuarios
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");

            // Obtiene el elemento raíz
            XmlElement rootElement = xmlDoc.DocumentElement;

            // Verifica si existe un usuario con el nombre juan y la contraseña andresmolina
            bool existeUsuario = verifyAux(rootElement, name, atributo);

            return existeUsuario;
        }

        /// <summary>
        /// Metodo auxiliar que veridica si existe un XML store
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="nombre_store"></param>
        /// <param name="atributos"></param>
        /// <returns></returns>
        public static bool verifyAux(XmlElement parentElement, string nombre_store, string atributos)
        {
            // Busca todos los elementos "Store" descendientes del elemento padre
            XmlNodeList storeElements = parentElement.SelectNodes("//Store");

            // Itera sobre los elementos "Store" y verifica el nombre
            foreach (XmlElement storeElement in storeElements)
            {
                //Obtiene el nombre de store del elemento en el que se encuentra
                XmlElement nameElement = storeElement.SelectSingleNode($"{nombre_store}/{atributos}") as XmlElement;

                //Verifica si son iguales
                if (nameElement != null && nameElement.Name == atributos)
                {
                    return true;
                }
            }
            return false;
        }
    }
}