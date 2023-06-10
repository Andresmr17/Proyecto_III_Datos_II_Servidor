class Hnode{
    private char data;
    private int frecuencia;
    private Hnode left;
    private Hnode right;
    private Hnode? next;
    public Hnode? getNext(){
        return this.next;
    }
    public void setNext(Hnode? nextx){
        this.next = nextx;
    }
    public Hnode getLeft(){
        return this.left;
    }
    public Hnode getRight(){
        return this.right;
    }
    public void insertRight(Hnode nodo){
        this.right = nodo;
    }
    public void insertLeft(Hnode nodo){
        this.left = nodo;
    }
    public int getFrecuencia(){
        return this.frecuencia;
    }
    public char getLetra(){
        return this.data;
    }
    public Hnode(int freq ,char dat){
        //me da errores inicializar left or right como null
        this.frecuencia=freq;
        this.data = dat;
    }
}