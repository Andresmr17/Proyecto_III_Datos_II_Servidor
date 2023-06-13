/// <summary>
/// Clase en la que se implementan los metodos para realizar el Hoffman code 
/// </summary>
class Hlist{
    
    private Hnode head;
    private Hnode? current;

    private int size;

    /// <summary>
    /// Metodo que inserta el primer elemento
    /// </summary>
    /// <param name="frequency"> Representa la frecuencia de la informacion </param>
    /// <param name="data"> Representa la informacion a usar </param>
    /// <returns> No retorna nada </returns>
    public void insertFirst(int frequency, char data) {
        if(size != 0){ /**si el tamaño de la lista es diferente de 0*/
            Hnode newnode = new Hnode(frequency, data); /**cree un nuevo nodo, sobrecargando la operacion new*/
            newnode.setNext(head); //el nodo siguiente es el head

            head = newnode;//cambia el head
            size++;
        }
        else{ /**si no, cree un nuevo nodo y asigneselo al puntero head*/
            this.head =new Hnode(frequency, data);
            size++;
        }
    }

    /// <summary>
    /// Metodo que obtiene la frecuencia
    /// </summary>
    /// <param> No tiene parametros </params>
    /// <returns> Retorna el Hnode actual </returns>
    public Hnode getminorFreq(){
        Hnode current2 = head;
        current = head;
        int menorFrec = head.getFrecuencia();
        for (int i=0;i<this.size;i++){
            if(menorFrec >= current.getFrecuencia()){
                menorFrec = current.getFrecuencia();
                current2=current;
            }
            
            current = current.getNext();
            //esto no me da el menor por que no guarda el nodo
            //del menor.
        }
        if(size !=0){
            //current = current2; //possible mistake here, this probable
        //gets an errroy by not assigning current properly check later.
            Deletes(current2);
            //return current;
        }
        else{
            Console.WriteLine("lista vacia");
        }
        return current2;
    }

    /// <summary>
    /// Metodo que se encarga de eliminar el Hnode indicado
    /// </summary>
    /// <param name="actual"> Representa el Hnode que se desea eliminar </param>
    /// <returns> No retorna nada </returns>
    public void Deletes(Hnode actual){
        //el delete deberia de servir 
        current = head;
        //actual.getNext();
    
        if(actual !=head){
            for(int i=0;i<size;i++){
                if(current.getNext() !=actual){
                    current = current.getNext();
                }
                else{
                    break;
                }
            }
            //Console.WriteLine("a borrar es:"+actual.getFrecuencia());
            //Console.WriteLine("el anterior es:"+current.getFrecuencia());
            current.setNext(actual.getNext()); //aca esta el error.
            size--;
            //esto funciona en las primeras iteraciones, lo cual
            //esta raro.
            //ACA ESTA EL ERRORR.
        }
        else{ //caso lo que quiero borrar es la cabeza
            head = head.getNext();
            size--;
        }
    }

    /// <summary>
    /// Metodo que printea la lista
    /// </summary>
    /// <param> No tiene parametros </params>
    /// <returns> No retorna nada </returns>
    public void printList(){
        current = head;
        for (int i=0;i<size;i++){
            Console.WriteLine(current.getLetra());
            current=current.getNext();
        }
    }

    /// <summary>
    /// Es el contructor de la clase HuffmanList
    /// </summary>
    /// <params> No tiene parametros </params>
    /// <returns> No tiene retornos </returns>
    public Hlist(){ //constructor de la clase HuffmanList.
        size=0;
    }

    /// <summary>
    /// Metodo que inserta el primer nodo
    /// </summary>
    /// <param name="frequence"> Representa la frecuencia de la informacion a comprimir </param>
    /// <param name="left"> Representa el nodo de la izquierda </param>
    /// <param name="right"> Representa el nodo de la derecha </param>
    /// <returns> No tiene retornos </returns>
    public void InsertNode(int frequence , Hnode left ,Hnode right){
        Hnode newNode = new Hnode(frequence,'$' );
        newNode.insertLeft(left);
        newNode.insertRight(right);
        if(size != 0){ /**si el tamaño de la lista es diferente de 0*/
            newNode.setNext(head); //el nodo siguiente es el head
            head = newNode;//cambia el head
            size++;
        }
        else{ /**si no, cree un nuevo nodo y asigneselo al puntero head*/
            this.head =newNode;
            size++;
        }

    }

    /// <summary>
    /// Metodo que obtiene el tamaño 
    /// </summary>
    /// <params> No tiene parametros </params>
    /// <returns> Retorna el tamaño </returns>
    public int getSize(){
        return this.size;
    }

    /// <summary>
    /// Metodo que obtiene el primer elemento 
    /// </summary>
    /// <params> No tiene parametros </params>
    /// <returns> Retorna el primer elemento </returns>
    public Hnode getFirst(){
        return head;
    }
}