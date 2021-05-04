using System.Threading.Tasks;

namespace BehaviorTree
{
    public enum NodeType {
        Condition,
        Action,
        Selector
    }

    public class Node
    {
        public Node prevNode;
        public Node nextNode;

        public NodeType nodeType;

        public Node()
        {
        }
    
        public Node(Node nextNode)
        {
            this.nextNode = nextNode;
            this.nextNode.prevNode = this;
        }
    
        public async virtual Task<bool> CheckCondition()
        {
            return false;
        }

        protected async Task<bool> ComputeNextNode()
        {
            if (nextNode != null)
            {
                return await nextNode.CheckCondition();
            }

            return true;
        }
    }
}