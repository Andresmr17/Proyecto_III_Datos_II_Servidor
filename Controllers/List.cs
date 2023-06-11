using System;
using System.IO;
using System.Xml;

class List
{
    private Node head;
    private Node current;

    private int size;

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

    public void PrintList()
    {
        current = head;

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine(current.GetLetter());
            current = current.GetNext();
        }
    }

    public List()
    {
        size = 0;
    }

    public int GetSize()
    {
        return size;
    }

    public Node GetFirst()
    {
        return head;
    }
}
