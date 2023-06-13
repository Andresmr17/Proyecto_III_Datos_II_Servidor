
class morseTrans{//clase que hara de traductor de morse a abc
    private Nodo current; //points to the current node
    private Nodo head; //points to the first node

    /// <summary>
    /// Metodo que desarrolla la traduccion del codigo morse a español
    /// </summary>
    public void buildTranslator(){
        //this array is an array of simbols with all the abc simbols
        //if you want to add , lets say numbers, you could, but you
        //must separate morse characters with '' empty char spaces.
        //the size of the array is practicaly the number of morse simbols
        //+ some empty spaces between them
        string[] morseSimbols = new string[26] {".-","-..."
        ,"-.-.","-..",".","..-.","--.","....","..",".---","-.-",
        ".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",
        ".--","-..-","-.--","--.."
        };
        string[] abecedario = new String[26]{"A","B","C","D","E","F","G","H",
        "I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
        };
        for(int a=0;a<26;a++){
            current = head;
            string currentMorse=morseSimbols[a];
            int size =currentMorse.Length;
            for(int b=0;b<size;b++){
                if(currentMorse[b] =='.'){ //si es punto
                    if(current.getRight()==null){
                        Nodo nodo = new Nodo();
                        nodo.setSimbol(currentMorse[b]);
                        current.insertRight(nodo);
                        current = nodo;
                    }
                    else{
                        current = current.getRight();
                    }
                }
                else{
                    if(current.getLeft()==null){
                        Nodo nodo = new Nodo();
                        nodo.setSimbol(currentMorse[b]);
                        current.insertLeft(nodo);
                        current = nodo;
                    }
                    else{
                        current = current.getLeft();
                    }
                }
            }
            current.setLetra(abecedario[a]);
        }

    }

    /// <summary>
    /// Metodo que llama al traducto
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> No retorna nada </returns>
    public morseTrans(){
        head = new Nodo();
        current = head;
        buildTranslator();
    }

    /// <summary>
    /// Metodo que printea el primer nodo
    /// </summary>
    /// /// <param> No tiene parametros </param>
    /// <returns> No retorna nada </returns>
    public void printFirst(){
        current=head.getRight();
        Console.WriteLine(current.getLetra());
    }

    /// <summary>
    /// Metodo que realiza la traduccion
    /// </summary>
    /// <param name="morsecode"> Representa el codigo en morse que se debe traducir </param>
    /// <returns> Retorna la letra traducida </returns>
    public string translate(string morsecode){
        current = head;
        string letra="";
        int size=morsecode.Length;
        for(int i=0;i<size;i++){
            if(morsecode[i]=='.'){
                current = current.getRight();
                //Console.WriteLine("right");
            }
            else if(morsecode[i]=='-'){
                current = current.getLeft();
                //Console.WriteLine("left");
            }
            else{//si no es ninguna de las 2, implica que
            //es un espacio vacio, por lo tanto debe de retornar al 
            //inicio de translate(head) para la siguiente letra
                if(i+1 !=size){
                    letra += current.getLetra();
                    current=head;  
                }
                else{
                    //do nothing.
                }
            }
        }
        letra += current.getLetra(); // SE DEBE DE EVALUAR CASO
        //EXCEPCIONAL DONDE LA ULTIMA LETRA PODRIA SER UN ESPACIO.
        Console.WriteLine(letra);
        return letra;

    }
}