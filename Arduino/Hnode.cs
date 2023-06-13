class Hnode{
    private char data;
    private int frecuencia;
    private Hnode left;
    private Hnode right;
    private Hnode? next;

    /// <summary>
    /// Metodo que obtiene el siguiente nodo
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna el siguiente nodo </returns>
    public Hnode? getNext(){
        return this.next;
    }
    
    /// <summary>
    /// Metodo que setea el siguiende nodo
    /// </summary>
    /// <param name="nextx"> Representa el nodo que se quiere poner de siguiente </param>
    /// <returns> No retorna nada </returns>
    public void setNext(Hnode? nextx){
        this.next = nextx;
    }

    /// <summary>
    /// Metodo que obtiene le nodo de la izquierda
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> retorna el nodo de la izquierda </returns>
    public Hnode getLeft(){
        return this.left;
    }

    /// <summary>
    /// Metodo que obtiene el nodo de la derecha
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna el nodo de la derecha </returns>
    public Hnode getRight(){
        return this.right;
    }

    /// <summary>
    /// Metodo que inserta un nodo a la derecha
    /// </summary>
    /// <param name="nodo"> Representa el nodo que se quiere insertar a la derecha </param>
    /// <returns> No retorna nada </returns>
    public void insertRight(Hnode nodo){
        this.right = nodo;
    }

    /// <summary>
    /// Metodo que inserta un nodo a la izquierda
    /// </summary>
    /// <param name="nodo"> Representa el nodo que se quiere insertar a la izquierda </param>
    /// <returns> No retorna nada </returns>
    public void insertLeft(Hnode nodo){
        this.left = nodo;
    }

    /// <summary>
    /// Metodo que obtiene el nodo la frecuencia
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna la frecuencia </returns>
    public int getFrecuencia(){
        return this.frecuencia;
    }

    /// <summary>
    /// Metodo que obtiene la letra
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna la letra que se quiere </returns>
    public char getLetra(){
        return this.data;
    }

    /// <summary>
    /// Metodo que setea la frecuencia y la data
    /// </summary>
    /// <param name="freq"> Representa la frecuencia que se quiere setear </param>
    /// <param name="dat"> Representa la informacion que se quiere setear </param>
    public Hnode(int freq ,char dat){
        //me da errores inicializar left or right como null
        this.frecuencia=freq;
        this.data = dat;
    }
}