using System;
using System.Collections.Generic;

namespace BehaviorTree
{
    public abstract class Condition : Node
    {
        public Condition()
        {
            // Conditions have no children
            children = new List<Node>();
        }

        public override NodeState Evaluate()
        {
            // Call the abstract Check method to determine the result of the condition
            bool result = Check();

            if (result)
            {
                state = NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.FAILURE;
            }

            return state;
        }

        // Override this method to implement the specific condition check
        protected abstract bool Check();

        // Add a public method to access the Check() method
        public bool IsConditionMet()
        {
            return Check();
        }
    }
}
