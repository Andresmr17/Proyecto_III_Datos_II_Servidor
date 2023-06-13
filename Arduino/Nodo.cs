class Nodo{
    private char morseSimbol; //simbolo en morse ya sea . o -
    private string letraAbecedario;// letra del abecedario
    private Nodo left;
    private Nodo right;

    /// <summary>
    /// Metodo que obtiene las letras del abecedario
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna la letra del abecedario </returns>
    public string getLetra(){//gets the abc simbol
        return this.letraAbecedario;
    }

    /// <summary>
    /// Metodo que obtiene el simbolo morse
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna el simbolo morse </returns>
    public char getmorse(){ //gets the morse simbol
        return this.morseSimbol;
    }

    /// <summary>
    /// Metodo que setea la letra del abecedario
    /// </summary>
    /// <param name="letra"> Representa la letra que se va a setear </param>
    /// <returns> No retorna nada </returns>
    public void setLetra(string letra){//sets the abc simbol
        this.letraAbecedario = letra;
    }

    /// <summary>
    /// Metodo que setea un simbolo morse
    /// </summary>
    /// <param name="simbol"> Representa el simbolo morse que se va a setear </param>
    /// /// <returns> No retorna nada </returns>
    public void setSimbol(char simbol){ //sets the morse simbol
        this.morseSimbol = simbol;
    }

    /// <summary>
    /// Metodo que obtiene el nodo de la izquierda
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna el nodo de la izquierda </returns>
    public Nodo getLeft(){
        return this.left;
    }

    /// <summary>
    /// Metodo que obtiene el nodo de la derecha
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna el nodo de la derecha </returns>
    public Nodo getRight(){
        return this.right;
    }

    /// <summary>
    /// Metodo que inserta el nodo a la derecha
    /// </summary>
    /// <param name="nodo"> Representa le nodo que se quiere isnertar a la derecha </param>
    /// /// <returns> No retorna nada </returns>
    public void insertRight(Nodo nodo){
        this.right = nodo;
    }

    /// <summary>
    /// Metodo que inserta el nodo a la izquierda
    /// </summary>
    /// <param name="nodo"> Representa le nodo que se quiere isnertar a la izquierda </param>
    /// /// <returns> No retorna nada </returns>
    public void insertLeft(Nodo nodo){
        this.left = nodo;
    }

    /// <summary>
    /// Metodo que setea de manera inicial informacion a los parametros morseSimbol y y letraAbecedario
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> No retorna nada </returns>
    public Nodo(){
        //me da errores inicializar left or right como null
        morseSimbol='ñ';
        letraAbecedario ="ñ";
    }
}