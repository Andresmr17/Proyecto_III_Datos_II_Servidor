using System.Xml;
namespace XmlsStore
{
    
    public class XmlStore_manager
    {
        
        public static void creates(string storename, string atributos)
        {
            
            // Crea un documento XML
            XmlDocument xmlDoc = new XmlDocument();

            // Crea el elemento raíz
            XmlElement rootElement = xmlDoc.CreateElement("Stores");
            xmlDoc.AppendChild(rootElement);

            // Crea un XMLStore
            XmlElement storeElement = xmlDoc.CreateElement("Store");
            rootElement.AppendChild(storeElement);

            // Añade el nombre del XMLStore
            XmlElement nameElement = xmlDoc.CreateElement("Nombre_Store");
            nameElement.InnerText = storename;
            storeElement.AppendChild(nameElement);

            //Añade los atributos del XMLStore
            XmlElement atributosElement = xmlDoc.CreateElement("Atributos");
            atributosElement.InnerText = atributos;
            storeElement.AppendChild(atributosElement);

            // Guarda el documento XML en un archivo
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");
            //../tuto-api/DB/Entities/User.json
            
            
            }

        public static void add(string storename, string atributos)
        {
            
            Console.WriteLine("Valor del nodo 'storename': " + storename);

            Console.WriteLine("Valor del nodo 'atributo': " + atributos);
        

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml");

            // Obtiene el elemento raíz
            XmlElement rootElement = xmlDoc.DocumentElement;

            // Crea un XMLStore
            XmlElement storeElement = xmlDoc.CreateElement("Store");
            rootElement.AppendChild(storeElement);

            // Añade el nombre del XMLStore
            XmlElement nameElement = xmlDoc.CreateElement("Nombre_Store");
            nameElement.InnerText = storename;
            storeElement.AppendChild(nameElement);

            //Añade los atributos del XMLStore
            XmlElement atributosElement = xmlDoc.CreateElement("Atributos");
            atributosElement.InnerText = atributos;
            storeElement.AppendChild(atributosElement);

            // Agrega el nuevo libro al elemento raíz
            rootElement.AppendChild(storeElement);

            // Guarda los cambios en el documento XML
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Stores/Stores.xml"); 
            
            }
        

        public static bool verify(string name, string atributo){
            
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
            // Busca todos los elementos "Usuario" descendientes del elemento padre
            XmlNodeList usuariosElements = parentElement.SelectNodes("//Store");

            // Itera sobre los elementos "Usuario" y verifica el nombre
            foreach (XmlElement storeElement in usuariosElements)
            {
                //Obtiene el nombre de usuario del elemento en el que se encuentra
                XmlElement nameElement = storeElement.SelectSingleNode("Nombre_Store") as XmlElement;

                //Obtiene la contraseña del elemento en el que se encuentra
                XmlElement atributosElement = storeElement.SelectSingleNode("Atributos") as XmlElement;

                //Verifica si son iguales
                if (nameElement != null && nameElement.InnerText == nombre_store && atributosElement != null && atributosElement.InnerText == atributos)
                {
                    return true;
                }
            }

            return false;
        }
    }
}