using BehaviorTree;
using UnityEngine.AI;
using UnityEngine;

public class BackAway : Node
{
    private NavMeshAgent _agent;
    private float _backAwayDistance;
    private float _cooldown;
    private float _lastCalled;

    public BackAway(NavMeshAgent agent, float backAwayDistance, float cooldown)
    {
        _agent = agent;
        _backAwayDistance = backAwayDistance;
        _cooldown = cooldown;
        _lastCalled = -cooldown;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Evaluate() in BackAway node");
        Transform target = (Transform)GetData("player");

        // Check if enough time has passed since the last call
        if (Time.time - _lastCalled < _cooldown)
        {
            return state;
        }

        Vector3 directionToTarget = _agent.transform.position - target.position;
        Vector3 newPosition = _agent.transform.position + directionToTarget.normalized * _backAwayDistance;

        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(newPosition, out navMeshHit, 2.0f, NavMesh.AllAreas))
        {
            Debug.Log("Setting new destination in BackAway node");
            _agent.SetDestination(navMeshHit.position);
            state = NodeState.RUNNING;
            _lastCalled = Time.time;
        }
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}
