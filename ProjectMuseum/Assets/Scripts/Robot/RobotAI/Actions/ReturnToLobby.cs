using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class ReturnToLobby : Node
{
    private NavMeshAgent _agent;
    private Transform _lobbySpawn;

    public ReturnToLobby(NavMeshAgent agent, Transform lobbySpawn)
    {
        _agent = agent;
        _lobbySpawn = lobbySpawn;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Setting destination to LobbySpawn in ReturnToLobby node");
        _agent.isStopped = false;
        _agent.SetDestination(_lobbySpawn.transform.position);
        state = NodeState.RUNNING;
        return state;
    }
}
