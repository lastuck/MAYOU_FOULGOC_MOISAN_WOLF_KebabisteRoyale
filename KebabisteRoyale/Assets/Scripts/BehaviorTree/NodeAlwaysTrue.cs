using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class NodeAlwaysTrue : Node
    {
        public NodeAlwaysTrue()
        {
            nodeType = NodeType.Condition;
        }

        public override async Task<bool> CheckCondition()
        {
            return await ComputeNextNode();
        }

        public NodeAlwaysTrue(Node nextNode) : base(nextNode)
        {
        }
    }
}