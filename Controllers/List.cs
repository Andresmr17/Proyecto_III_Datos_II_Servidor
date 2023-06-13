using System;
using System.IO;
using System.Xml;

class List
{
    private Node head;
    private Node current;

    private int size;

    /// <summary>
    /// Metodo que inserta el primer elemento XML 
    /// </summary>
    /// <param name="data"> Informacion que se desea insertar </param>
    /// <returns> No retorna nada </returns>
    public void InsertFirst(XmlNode data)
    {
        if (size != 0)
        {
            Node newNode = new Node(data);
            newNode.SetNext(head);
            head = newNode;
            size++;
        }
        else
        {
            head = new Node(data);
            size++;
        }
    }

    /// <summary>
    /// Metodo que elimina un nodo
    /// </summary>
    /// <param name="actual"> nodo que se desea eliminar </param>
    /// /// <returns> No retorna nada </returns>
    public void Deletes(Node actual)
    {
        current = head;

        if (actual != head)
        {
            for (int i = 0; i < size; i++)
            {
                if (current.GetNext() != actual)
                {
                    current = current.GetNext();
                }
                else
                {
                    break;
                }
            }

            current.SetNext(actual.GetNext());
            size--;
        }
        else
        {
            head = head.GetNext();
            size--;
        }
    }
    
    /// <summary>
    /// Metodo que printea la lista
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> No retorna nada </returns>
    public void PrintList()
    {
        current = head;

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine(current.GetLetter());
            current = current.GetNext();
        }
    }

    /// <summary>
    /// Metodo que instancia una lista
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> No retorna nada </returns>
    public List()
    {
        size = 0;
    }

    /// <summary>
    /// Metodo que obtiene el tamaño de la lista
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna el tamaño de la lista</returns>
    public int GetSize()
    {
        return size;
    }

    /// <summary>
    /// Metodo que obtiene el primer elemento nodo
    /// </summary>
    /// <param> No tiene parametros </param>
    /// <returns> Retorna el primer nodo </returns>
    public Node GetFirst()
    {
        return head;
    }
}
