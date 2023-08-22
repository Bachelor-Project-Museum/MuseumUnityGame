using BehaviorTree;
using UnityEngine.AI;
using UnityEngine;

public class FollowPlayer : Node
{
    private readonly Transform _agentTransform;
    private readonly Transform _playerTransform;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly float _followDistance;

    public FollowPlayer(Transform agentTransform, Transform playerTransform, NavMeshAgent navMeshAgent, float followDistance)
    {
        _agentTransform = agentTransform;
        _playerTransform = playerTransform;
        _navMeshAgent = navMeshAgent;
        _followDistance = followDistance;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Evaluate() in FollowPlayer node");
        try
        {
            // Set the destination to a point behind the player
            Vector3 targetPosition = _playerTransform.position - (_playerTransform.forward * _followDistance);
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(targetPosition);

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
