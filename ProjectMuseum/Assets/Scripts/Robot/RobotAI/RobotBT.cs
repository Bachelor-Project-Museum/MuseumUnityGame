using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class RobotBT : BehaviorTree.Tree
{
    [Header("Robot Variables")]
    public float speed = 2f;
    public float followDistance = 5f;
    public float backAwayDistance = 1.5f;
    public float backAwayCooldown = 3f;

    private FollowPlayer _followPlayerNode;
    private BackAway _backAwayNode;
    private Condition _followCondition;

    private GameManager _gameManager; // Reference to the GameManager script

    protected override void StartBehaviorTree()
    {
        _gameManager = FindObjectOfType<GameManager>(); // Find the GameManager script in the scene

        NavMeshAgent agent = GetComponent<NavMeshAgent>(); // Find the NavMeshAgent attached to the robot

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found");
            return;
        }

        Debug.Log("NavMeshAgent found");

        // Create FollowPlayer and BackAway nodes
        _followPlayerNode = new FollowPlayer(agent, speed);
        _backAwayNode = new BackAway(agent, backAwayDistance, backAwayCooldown);

        Debug.Log("FollowPlayer node created");
        Debug.Log("BackAway node created");

        // Create a condition node to check if the robot should follow the player
        _followCondition = new CustomCondition(() => _gameManager.RobotFollow);

        // Create an Inverter node to invert the result of the _followCondition
        var invertedFollowCondition = new Inverter(_followCondition);

        // Create root node with a Selector node and two Sequence nodes
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                _followCondition,
                new FollowDistance(transform, _gameManager.PlayerObject.transform, agent, followDistance),
                _followPlayerNode,
            }),
            new Sequence(new List<Node>
            {
                invertedFollowCondition,
                new ReturnToLobby(agent, _gameManager.LobbySpawn.transform),
            }),
            _backAwayNode,
        });

        _root = root;
    }

    protected override void Update()
    {
        base.Update();

        // Update player data on each tick
        _root.SetData("player", _gameManager.PlayerObject.transform);

        // Check if shouldFollow value has changed in the GameManager during runtime
        if (_followCondition != null && _followCondition.IsConditionMet() != _gameManager.RobotFollow)
        {
            // Recreate the behavior tree if shouldFollow value has changed
            RecreateBehaviorTree();
        }
    }

    private void RecreateBehaviorTree()
    {
        // Clear the children of the root node
        _root.children.Clear();

        // Create a condition node to check if the robot should follow the player
        _followCondition = new CustomCondition(() => _gameManager.RobotFollow);

        // Reorder the nodes to match the updated structure
        _root.children.Add(_followCondition);
        _root.children.Add(new Sequence(new List<Node>
        {
            new FollowDistance(transform, _gameManager.PlayerObject.transform, GetComponent<NavMeshAgent>(), followDistance),
            _followPlayerNode,
        }));
        _root.children.Add(_backAwayNode);
    }


}
