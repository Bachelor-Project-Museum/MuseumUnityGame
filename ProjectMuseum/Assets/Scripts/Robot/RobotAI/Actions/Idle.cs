using BehaviorTree;
using UnityEngine.AI;
using UnityEngine;

public class Idle : Node
{
    private readonly NavMeshAgent _navMeshAgent;

    public Idle(NavMeshAgent navMeshAgent)
    {
        _navMeshAgent = navMeshAgent;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Evaluate() in Idle node");
        try
        {
            // Stop the NavMeshAgent from moving
            _navMeshAgent.isStopped = true;

            return NodeState.SUCCESS;
        }
        catch (System.Exception)
        {
            // Stop the NavMeshAgent from moving
            _navMeshAgent.isStopped = true;
            return NodeState.FAILURE;
        }
    }
}
