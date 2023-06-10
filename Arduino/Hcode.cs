class Hcode{
    private Hlist lista;
    private String mensaje;
    public Hcode(){

    }
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
    public void printcodes(Hnode raiz , string str, char letra){
        if (raiz == null)
            return;
 
        if (raiz.getLetra()== letra){
            mensaje=mensaje + str;
            
        }
        printcodes(raiz.getLeft(), str + "0" , letra);
        printcodes(raiz.getRight(), str + "1" , letra);
    }
    public string compressedWord(char[] word){
        mensaje="";
        for(int i=0;i<word.Length;i++){
            printcodes(lista.getFirst(),"",word[i]);
        }
        return mensaje;
    }
    public void printTree(Hnode raiz , string str){
        if (raiz == null)
            return;
 
        if (raiz.getLetra()!= '$'){
            Console.WriteLine(raiz.getLetra()+":"+str);
            
        }
        printTree(raiz.getLeft(), str + "0" );
        printTree(raiz.getRight(), str + "1");
    }
    public void printTreeword(Hnode raiz ){
        if (raiz == null)
            return;
 
        else{
            Console.WriteLine(raiz.getLetra());
            
        }
        printTreeword(raiz.getLeft() );
        printTreeword(raiz.getRight());
    }
    public void clearmsg(){
        this.mensaje ="";
    }
}