using System;

namespace BehaviorTree
{
    public class CustomCondition : Condition
    {
        private Func<bool> condition;

        public CustomCondition(Func<bool> condition)
        {
            this.condition = condition;
        }

        protected override bool Check()
        {
            return condition.Invoke();
        }
    }
}
