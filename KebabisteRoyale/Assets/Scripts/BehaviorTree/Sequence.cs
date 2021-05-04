using System;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class SequenceAction : Node
    {
        private Action actionToExecute;
        private bool isRunning;
        private Func<bool> checkCanContinueLambda;
    
        public SequenceAction(Action actionToExecute, Func<bool> checkCanContinueLambda = null) : base()
        {
            this.checkCanContinueLambda = checkCanContinueLambda;
            this.actionToExecute = actionToExecute;
            nodeType = NodeType.Action;
        }
    
        public SequenceAction(Action actionToExecute, Node nextNode, Func<bool> checkCanContinueLambda = null) : base(nextNode)
        {
            this.checkCanContinueLambda = checkCanContinueLambda;
            this.actionToExecute = actionToExecute;
            nodeType = NodeType.Action;
        }

        public override async Task<bool> CheckCondition()
        {
            isRunning = true;
            actionToExecute?.Invoke();

            while (isRunning && checkCanContinueLambda != null && checkCanContinueLambda())
            {
                await Task.Delay(10);
            }

            return await ComputeNextNode();
        }
    }

    public class SequenceCondition : Node
    {
        private Func<bool> lambada;

        public SequenceCondition(Func<bool> nLambada)
        {
            lambada = nLambada;
            nodeType = NodeType.Condition;
        }

        public SequenceCondition(Func<bool> nLambada, Node nextNode) : base(nextNode)
        {
            lambada = nLambada;
            nodeType = NodeType.Condition;
        }
    
        public override async Task<bool> CheckCondition()
        {
            if (lambada != null && lambada())
            {
                return await ComputeNextNode();
            }

            return false;
        }
    }
}