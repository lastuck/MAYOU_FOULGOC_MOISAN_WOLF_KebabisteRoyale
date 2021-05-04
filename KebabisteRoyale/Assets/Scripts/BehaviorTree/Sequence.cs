using System;

public class SequenceAction<T> : Node<T>
{
    public SequenceAction(Func<T> actionToExecute) : base(actionToExecute)
    {
        nodeType = NodeType.Action;
    }
    
    public SequenceAction(Func<T> actionToExecute, Node<T> nextNode) : base(actionToExecute, nextNode)
    {
        nodeType = NodeType.Action;
    }
}

public class SequenceCondition<T> : Node<T>
{
    public SequenceCondition(Func<bool> nLambada) : base(nLambada)
    {
        nodeType = NodeType.Condition;
    }

    public SequenceCondition(Func<bool> nLambada, Node<T> nextNode) : base(nLambada, nextNode)
    {
        nodeType = NodeType.Condition;
    }
}