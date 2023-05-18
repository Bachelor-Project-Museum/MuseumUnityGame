using System.Collections.Generic;

namespace BehaviorTree
{
    public class Inverter : Node
    {
        private Node _child;

        public Inverter(Node child)
        {
            _child = child;
        }

        public override NodeState Evaluate()
        {
            NodeState childState = _child.Evaluate();

            if (childState == NodeState.SUCCESS)
                state = NodeState.FAILURE;
            else if (childState == NodeState.FAILURE)
                state = NodeState.SUCCESS;
            else
                state = childState;

            return state;
        }
    }
}
