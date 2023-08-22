using BehaviorTree;
using UnityEngine.AI;
using UnityEngine;

public class BackAway : Node
{
    private NavMeshAgent _agent;
    private float _backAwayDistance;

    public BackAway(NavMeshAgent agent, float backAwayDistance)
    {
        _agent = agent;
        _backAwayDistance = backAwayDistance;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Evaluate() in BackAway node");
        Transform target = (Transform)GetData((string)GameManager.Instance.PlayerObject.name);


        Vector3 directionToTarget = _agent.transform.position - target.position;
        //Vector3 newPosition = _agent.transform.position + directionToTarget.normalized * _backAwayDistance; normalized?????????????????
        Vector3 newPosition = _agent.transform.position + directionToTarget * _backAwayDistance;

        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(newPosition, out navMeshHit, 2.0f, NavMesh.AllAreas))
        {
            Debug.Log("Setting new destination in BackAway node");
            _agent.isStopped = false;
            _agent.SetDestination(navMeshHit.position);
            state = NodeState.RUNNING;
        }
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}
