using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public List<Node> sequences;

        public Selector()
        {
            sequences = new List<Node>();
            nodeType = NodeType.Selector;
        }

        public void AddNode(Node node)
        {
            node.prevNode = this;
            sequences.Add(node);
        }

        public override async Task<bool> CheckCondition()
        {
            foreach (Node node in sequences)
            {
                if (node.nodeType == NodeType.Action)
                {
                    Debug.LogError("Un noeud action ne peut pas être dans un selector");
                    return false;
                }

                if (await node.CheckCondition())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
