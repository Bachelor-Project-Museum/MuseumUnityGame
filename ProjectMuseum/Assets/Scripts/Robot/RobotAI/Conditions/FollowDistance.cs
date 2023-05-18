using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class FollowDistance : Node
{
    private readonly Transform _agentTransform;
    private readonly Transform _playerTransform;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly float _followDistance;

    public FollowDistance(Transform agentTransform, Transform playerTransform, NavMeshAgent navMeshAgent, float followDistance)
    {
        _agentTransform = agentTransform;
        _playerTransform = playerTransform;
        _navMeshAgent = navMeshAgent;
        _followDistance = followDistance;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Evaluate() in FollowDistance node");
        float distance = Vector3.Distance(_agentTransform.position, _playerTransform.position);

        if (distance >= _followDistance)
        {
            // Set the destination to a point behind the player
            Vector3 targetPosition = _playerTransform.position - (_playerTransform.forward * _followDistance);
            _navMeshAgent.SetDestination(targetPosition);

            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
