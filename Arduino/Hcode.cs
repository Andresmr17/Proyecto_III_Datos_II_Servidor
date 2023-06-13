/// <summary>
/// Clase en la que desarrollan métodos para el hoffman code
/// </summary>
class Hcode{
    private Hlist lista;
    private String mensaje;
    public Hcode(){

    }

    /// <summary>
    /// Metodo que se usa para realizar la compresion de la informacion
    /// </summary>
    /// <param name="data"> Respresenta la informacion. </param>
    /// <param name="freq"> Representa la frecuenta que presenta la informacion a comprimir</param>
    /// <param name="size"> Representa el tamaño de la informacion que se desea comprimir </param>
    ///  <returns> No tiene retorno </returns>
    public void comprimir(char[] data, int[] freq, int size){
        Hnode left,right,top;
        lista=new Hlist();
        for (int i = 0; i < size; ++i){
            lista.insertFirst(freq[i],data[i]);
        }
        while(lista.getSize()!=1){
            left = lista.getminorFreq();
            right = lista.getminorFreq();
            Console.WriteLine("left es:"+left.getLetra());
            Console.WriteLine("right es:"+right.getLetra());
            // Create a new internal node with frequency equal to the sum of the two nodes frequencies.
            // Make the two extracted node as left and right children of this new node.
            // Add this node to the min heap '$' is a special value for internal nodes, not used.
            lista.InsertNode(left.getFrecuencia()+right.getFrecuencia(),left,right);
        }
        //printTree(lista.getFirst(),"");
        printTreeword(lista.getFirst());
    }

    /// <summary>
    /// Metodo que realiza la impresion del codigo de la informacion ya comprimida
    /// </summary>
    /// <param name="raiz"> Representa la raiz de la que se forma el codigo </param>
    /// <param name="str"> Representa la palabra que se desea comprimir </param>
    /// <param name="letra"> Representa la letra que se esta traduciendo </param>
    /// <returns> No retorna nada </returns>
    public void printcodes(Hnode raiz , string str, char letra){
        if (raiz == null)
            return;
 
        if (raiz.getLetra()== letra){
            mensaje=mensaje + str;
            
        }
        printcodes(raiz.getLeft(), str + "0" , letra);
        printcodes(raiz.getRight(), str + "1" , letra);
    }

    /// <summary>
    /// Metodo que comprime una palabra
    /// </summary>
    /// <param name="word"> Representa la palabra que se desea comprimir </param>
    /// <returns> Retorna la palabra comprimida </returns>
    public string compressedWord(char[] word){
        mensaje="";
        for(int i=0;i<word.Length;i++){
            printcodes(lista.getFirst(),"",word[i]);
        }
        return mensaje;
    }

    /// <summary>
    /// Metodo que printea el arbol
    /// </summary>
    /// <param name="raiz"> Representa la raiz del arbol </param>
    /// <param name="str"> Representa la informacion de la que se hara el arbol </param>
    /// <returns> No retorna nada </returns>
    public void printTree(Hnode raiz , string str){
        if (raiz == null)
            return;
 
        if (raiz.getLetra()!= '$'){
            Console.WriteLine(raiz.getLetra()+":"+str);
            
        }
        printTree(raiz.getLeft(), str + "0" );
        printTree(raiz.getRight(), str + "1");
    }

    /// <summary>
    /// Metodo que retorna el arbol de letras
    /// </summary>
    /// <param name="raiz"> Representa la raiz del arbol que se va a mostrar </param>
    /// <returns> No retorna nada </returns>
    public void printTreeword(Hnode raiz ){
        if (raiz == null)
            return;
 
        else{
            Console.WriteLine(raiz.getLetra());
            
        }
        printTreeword(raiz.getLeft() );
        printTreeword(raiz.getRight());
    }

    /// <summary>
    /// Metodo que limpia la informacion al terminar el proceso de compresion
    /// </summary>
    public void clearmsg(){
        this.mensaje ="";
    }
}