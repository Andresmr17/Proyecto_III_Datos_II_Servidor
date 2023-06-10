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
            //JSONManager.AddToJSON<User>(newUser, "../tuto-api/DB/Entities/User.json");
            return Ok();
            //return BadRequest("La sentencia es incorrecta");
        }

        [HttpGet]
        [Route("login/{username}/{password}")]
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