using System.Xml;
namespace Xmls
{
    /// <summary>
    /// Clase en la que se implementan los metodos para guardar la informacion de los usuarios en formato XML
    /// </summary>
    public class Xml_manager
    {
        /// <summary>
        /// Metodo que crea un documento XML con la informacion suministrada
        /// </summary>
        /// <param name="username"> Representa el username de la persona a registrar </param>
        /// <param name="email"> representa el correo de la persona a registrar </param>
        /// <param name="password"> Representa la contraseña de la persona a registrar </param>
        /// /// <returns> No retorna nada </returns>
        public static void creates(string username, string email , string password)
        {
            
            // Crea un documento XML
            XmlDocument xmlDoc = new XmlDocument();

            // Crea el elemento raíz
            XmlElement rootElement = xmlDoc.CreateElement("Usuarios");
            xmlDoc.AppendChild(rootElement);

            // Crea un usuario
            XmlElement usuarioElement = xmlDoc.CreateElement("Usuario");
            rootElement.AppendChild(usuarioElement);

            // Añade el nombre del usuario
            XmlElement nombreElement = xmlDoc.CreateElement("Nombre_Usuario");
            nombreElement.InnerText = username;
            usuarioElement.AppendChild(nombreElement);

            // Añade el correo del usuario
            XmlElement correoElement = xmlDoc.CreateElement("Correo");
            correoElement.InnerText = email;
            usuarioElement.AppendChild(correoElement);

            //Añade la contraseña del usuario
            XmlElement contraseñaElement = xmlDoc.CreateElement("Contraseña");
            contraseñaElement.InnerText = password;
            usuarioElement.AppendChild(contraseñaElement);

            // Guarda el documento XML en un archivo
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Users/Users.xml");
            //../tuto-api/DB/Entities/User.json
            
            
            }

        /// <summary>
        /// Metodo que añade la informacion de un usuario que se desea registrar al archivo XML en donde se guardan los usuarios
        /// </summary>
        /// <param name="username"> Representa el username de la persona a registrar </param>
        /// <param name="email"> representa el correo de la persona a registrar </param>
        /// <param name="password"> Representa la contraseña de la persona a registrar </param>
        /// /// <returns> No retorna nada </returns>
        public static void add(string username, string email , string password)
        {
            
            Console.WriteLine("Valor del nodo 'username': " + username);

            Console.WriteLine("Valor del nodo 'email': " + email);

            Console.WriteLine("Valor del nodo 'password': " + password);
        

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../Proyecto_III_Datos_II_Servidor/Xmls/Users/Users.xml");

            // Obtiene el elemento raíz
            XmlElement rootElement = xmlDoc.DocumentElement;

            // Crea un usuario
            XmlElement usuarioElement = xmlDoc.CreateElement("Usuario");
            rootElement.AppendChild(usuarioElement);

            // Añade el nombre del usuario
            XmlElement nombreElement = xmlDoc.CreateElement("Nombre_Usuario");
            nombreElement.InnerText = username;
            usuarioElement.AppendChild(nombreElement);

            // Añade el correo del usuario
            XmlElement correoElement = xmlDoc.CreateElement("Correo");
            correoElement.InnerText = email;
            usuarioElement.AppendChild(correoElement);

            //Añade la contraseña del usuario
            XmlElement contraseñaElement = xmlDoc.CreateElement("Contraseña");
            contraseñaElement.InnerText = password;
            usuarioElement.AppendChild(contraseñaElement);

            // Agrega el nuevo libro al elemento raíz
            rootElement.AppendChild(usuarioElement);

            // Guarda los cambios en el documento XML
            xmlDoc.Save("../Proyecto_III_Datos_II_Servidor/Xmls/Users/Users.xml"); 
            
            }

        /// <summary>
        /// Metodo que verifica cuando se quiere iniciar sesion si existe un usuario con la contraseña insertada
        /// </summary>
        /// <param name="nombre"> Nombre de la persona que quiere ingresar </param>
        /// <param name="contraseña"> Contraseña de la persona que desea ingresar </param>
        /// <returns> Retorna un valor booleano </returns>
        public static bool verify(string nombre, string contraseña){
            
            // Carga el documento XML existente de usuarios
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../Proyecto_III_Datos_II_Servidor/Xmls/Users/Users.xml");

            // Obtiene el elemento raíz
            XmlElement rootElement = xmlDoc.DocumentElement;

            // Verifica si existe un usuario con el nombre juan y la contraseña andresmolina
            bool existeUsuario = verifyAux(rootElement, nombre, contraseña);

            return existeUsuario;
        }

        /// <summary>
        /// Metodo auxiliar que verifica la existencia de un usuario
        /// </summary>
        /// <param name="parentElement"> Representa la raiz del documento XML </param>
        /// <param name="nombre"> Nombre de la persona que quiere ingresar </param>
        /// <param name="contraseña"> Contraseña de la persona que desea ingresar </param>
        /// <returns> Retorna un valor booleano </returns>
        public static bool verifyAux(XmlElement parentElement, string nombre_usuario, string contraseña)
        {
            // Busca todos los elementos "Usuario" descendientes del elemento padre
            XmlNodeList usuariosElements = parentElement.SelectNodes("//Usuario");

            // Itera sobre los elementos "Usuario" y verifica el nombre
            foreach (XmlElement usuarioElement in usuariosElements)
            {
                //Obtiene el nombre de usuario del elemento en el que se encuentra
                XmlElement nombreElement = usuarioElement.SelectSingleNode("Nombre_Usuario") as XmlElement;

                //Obtiene la contraseña del elemento en el que se encuentra
                XmlElement contraseñaElement = usuarioElement.SelectSingleNode("Contraseña") as XmlElement;

                //Verifica si son iguales
                if (nombreElement != null && nombreElement.InnerText == nombre_usuario && contraseñaElement != null && contraseñaElement.InnerText == contraseña)
                {
                    return true;
                }
            }

            return false;
        }
    }
}