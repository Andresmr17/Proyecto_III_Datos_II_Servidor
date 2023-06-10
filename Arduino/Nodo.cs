class Nodo{
    private char morseSimbol; //simbolo en morse ya sea . o -
    private string letraAbecedario;// letra del abecedario
    private Nodo left;
    private Nodo right;
    public string getLetra(){//gets the abc simbol
        return this.letraAbecedario;
    }
    public char getmorse(){ //gets the morse simbol
        return this.morseSimbol;
    }
    public void setLetra(string letra){//sets the abc simbol
        this.letraAbecedario = letra;
    }
    public void setSimbol(char simbol){ //sets the morse simbol
        this.morseSimbol = simbol;
    }
    public Nodo getLeft(){
        return this.left;
    }
    public Nodo getRight(){
        return this.right;
    }
    public void insertRight(Nodo nodo){
        this.right = nodo;
    }
    public void insertLeft(Nodo nodo){
        this.left = nodo;
    }
    public Nodo(){
        //me da errores inicializar left or right como null
        morseSimbol='ñ';
        letraAbecedario ="ñ";
    }
}