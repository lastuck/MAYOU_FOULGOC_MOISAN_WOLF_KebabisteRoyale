namespace BehaviorTree
{
    public class NodeAlwaysTrue<T> : Node<T>
    {
        public NodeAlwaysTrue()
        {
            lambada = () =>
            {
                return true;
            };
        }
        
        public NodeAlwaysTrue(Node<T> nextNode) : base(nextNode)
        {
            lambada = () =>
            {
                return true;
            };
        }
    }
}