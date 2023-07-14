using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class FollowPlayer : Node
{
    private NavMeshAgent _agent;
    private float _speed;

    public FollowPlayer(NavMeshAgent agent, float speed)
    {
        _agent = agent;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Evaluate() in FollowPlayer node");
        Transform target = (Transform)GetData((string)GameManager.Instance.PlayerObject.name);

        if (Vector3.Distance(_agent.transform.position, target.position) > 0.01f)
        {
            _agent.speed = _speed;
            _agent.SetDestination(target.position);
            state = NodeState.RUNNING;
        }
        else
        {
            _agent.speed = 0f;
            state = NodeState.SUCCESS;
        }

        return state;
    }
}