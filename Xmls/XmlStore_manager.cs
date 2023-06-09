using System.Xml;

namespace XmlsStore
{
    public class XmlStoreManager
    {
        public static void creates(string storename, string atributos)
        {
            // Crea un documento XML
            XmlDocument xmlDoc = new XmlDocument();

            // Crea el elemento raíz "Stores"
            XmlElement rootElement = xmlDoc.CreateElement("Stores");
            xmlDoc.AppendChild(rootElement);

            // Crea un elemento "Store"
            XmlElement storeElement = xmlDoc.CreateElement("Store");
            rootElement.AppendChild(storeElement);

            // Añade el elemento storename + "S"
            XmlElement storeNamePluralElement = xmlDoc.CreateElement(storename + "S");
            storeElement.AppendChild(storeNamePluralElement);

            // Añade el elemento storename
            XmlElement storeNameElement = xmlDoc.CreateElement(storename);
            storeNamePluralElement.AppendChild(storeNameElement);

            // Crea los elementos de los atributos
            string[] lista_atributos = atributos.Split(",");
            foreach (string atributo in lista_atributos)
            {
                XmlElement atributoElement = xmlDoc.CreateElement(atributo);
                atributoElement.InnerText = atributo;
                storeNameElement.AppendChild(atributoElement);
            }

            // Guarda el documento XML en un archivo
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");
        }

        public static void add(string storename, string atributos)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");

            // Obtiene el elemento raíz "Stores"
            XmlElement rootElement = xmlDoc.DocumentElement;

            // Busca el elemento storename + "S"
            XmlElement storeNamePluralElement = rootElement.SelectSingleNode($"Store/{storename}S") as XmlElement;

            if (storeNamePluralElement == null)
            {
                // Si no existe el elemento storename + "S", crea uno nuevo
                storeNamePluralElement = xmlDoc.CreateElement(storename + "S");
                rootElement.SelectSingleNode("Store").AppendChild(storeNamePluralElement);
            }

            // Añade el elemento storename
            XmlElement storeNameElement = xmlDoc.CreateElement(storename);
            storeNamePluralElement.AppendChild(storeNameElement);

            // Crea los elementos de los atributos
            string[] lista_atributos = atributos.Split(",");
            foreach (string atributo in lista_atributos)
            {
                XmlElement atributoElement = xmlDoc.CreateElement(atributo);
                atributoElement.InnerText = atributo;
                storeNameElement.AppendChild(atributoElement);
            }

            // Guarda los cambios en el documento XML
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");
        }

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