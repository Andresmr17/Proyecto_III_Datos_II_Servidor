using Xmls;
using Microsoft.AspNetCore.Mvc;

namespace userControllers {

    [ApiController]
    [Route("user")]
    
    public class UserController : ControllerBase {
        private morseTrans morse= new morseTrans();
        private Hcode huffman = new Hcode();
        [HttpPost]
        [Route("register/{username}/{email}/{password}")]
        /// <summary>
        /// Metodo encargado de registrar a un usuario
        /// </summary>
        /// <param name="username"> Nombre de usuario con el que se desean registrar </param>
        /// <param name="email"> Correo del cliente que se desea registar </param>
        /// <param name="password"> Contraseña de la persona que se desea registrar </param>
        /// <returns> Retorna un valor propio de la clase ControllerBase </returns>
        public IActionResult Register(string username, string email, string password) {
            //CODIGO DE ARDUINO ABAJO, DOCUMENTAR SI SE VA A PROBAR
            //SIN EL ARDUINO**************
            password=morse.translate(password);
            char[] arr = new string(password.Distinct().ToArray()).ToCharArray();
            int[] freq = new int[arr.Length];
            for(int i=0;i<arr.Length;i++){
                int count = password.Count(x => x == arr[i]);
                freq[i]=count;
            }
            int size = arr.Length;
            huffman.comprimir(arr,freq,size); //esto me hace el arbol.
            password=huffman.compressedWord(password.ToCharArray());
            huffman.clearmsg();
            //END OF CODIGO ARDUINO********************
            Xml_manager.add(username, email, password);
            return Ok();
        }

        [HttpGet]
        [Route("login/{username}/{password}")]
        /// <summary>
        /// Metodo encargado de verificar que verificar que existe un usuario registrado
        /// </summary>
        /// <param name="username"> Nombre de usuario de la persona registrada </param>
        /// <param name="password"> Contraseña de la persona registrada </param>
        /// <returns> Retorna un valor propio de la clase ControllerBase </returns>
        public IActionResult login(string username, string password) {
            //CODIGO DE ARDUINO ABAJO, DOCUMENTAR SI SE VA A PROBAR
            //SIN EL ARDUINO**************
            password=morse.translate(password);
            char[] arr = new string(password.Distinct().ToArray()).ToCharArray();
            int[] freq = new int[arr.Length];
            for(int i=0;i<arr.Length;i++){
                int count = password.Count(x => x == arr[i]);
                freq[i]=count;
            }
            int size = arr.Length;
            huffman.comprimir(arr,freq,size); //esto me hace el arbol.
            password=huffman.compressedWord(password.ToCharArray());
            huffman.clearmsg();
            //END OF CODIGO ARDUINO********************
            
            if(Xml_manager.verify(username ,password)){

                var data = new {JsonResult = "Hola esta informacion se manda desde el backend"};

                return Ok(data);
            }
            else{
                return BadRequest();
            }
        }
    }
}