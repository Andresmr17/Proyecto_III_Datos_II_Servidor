using System;
using System.IO;
using System.Xml;

class Node
{
    private XmlNode data;
    private Node left;
    private Node right;
    private Node next;

    public Node GetNext()
    {
        return next;
    }

    public void SetNext(Node next)
    {
        this.next = next;
    }

    public Node GetLeft()
    {
        return left;
    }

    public Node GetRight()
    {
        return right;
    }

    public void InsertRight(Node node)
    {
        right = node;
    }

    public void InsertLeft(Node node)
    {
        left = node;
    }


    public XmlNode GetLetter()
    {
        return data;
    }

    public Node(XmlNode dat)
    {
        data = dat;
    }
}
