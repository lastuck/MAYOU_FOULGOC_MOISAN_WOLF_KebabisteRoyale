using System;

public enum NodeType {
    Condition,
    Action,
    Selector
}

public class Node<T>
{
    protected Func<bool> lambada;
    public Node<T> nextNode;

    protected Func<T> actionToExecute;

    public NodeType nodeType;

    public Node()
    {
        lambada = () =>
        {
            return false;
        };
    }
    
    public Node(Node<T> nextNode)
    {
        this.nextNode = nextNode;
    }

    public Node(Func<T> nActionToExecute)
    {
        actionToExecute = nActionToExecute;
    }

    public Node(Func<bool> nLambada)
    {
        lambada = nLambada;
    }

    public Node(Func<bool> nLambada, Node<T> nextNode)
    {
        lambada = nLambada;
        this.nextNode = nextNode;
    }
    
    public Node(Func<T> nActionToExecute, Node<T> nextNode)
    {
        actionToExecute = nActionToExecute;
        this.nextNode = nextNode;
    }

    public virtual bool CheckCondition()
    {
        return lambada();
    }
    
    public T PlayAction()
    {
        return actionToExecute();
    }
}
