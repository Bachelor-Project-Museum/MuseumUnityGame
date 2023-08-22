using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class RobotBT : BehaviorTree.Tree
{
    [Header("Robot Variables")]
    [SerializeField, Range(1, 20)] public float speed = 2f;
    [SerializeField, Range(5, 20)] private float followDistance = 5f; // The distance at which the robot will stop following the player
    [SerializeField, Range(1, 4.99f)] private float backAwayDistance = 3f; // The distance at which the robot will start backing away from the player
    [SerializeField] public bool followPlayer { get; set; } = true;

    private BackAway _backAwayNode;
    private Condition _followCondition;
    private Condition _followDistanceCondition;
    private Condition _backAwayCondition;
    private Inverter invertedFollowCondition;
    private Inverter invertedFollowDistanceCondition;
    private Node root;

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

        var player = _gameManager.PlayerObject; // Define the player from the GameManager script
        var robot = _gameManager.RoboterObject; // Define the robot from the GameManager script

        // Create FollowPlayer and BackAway nodes
        //_followPlayerNode = new FollowPlayer(agent, speed);
        _backAwayNode = new BackAway(agent, backAwayDistance);

        // Create a condition node to check if the robot should follow the player
        _followCondition = new CustomCondition(() => _gameManager.RobotFollow);
        // Create a condition node to check if the player is too far from the robot
        _followDistanceCondition = new CustomCondition(() =>
        {
            return Vector3.Distance(player.transform.position, robot.transform.position) > followDistance;
        });
        // Create a condition node to check if the robot should move back
        _backAwayCondition = new CustomCondition(() =>
        {
            if (Vector3.Distance(player.transform.position, robot.transform.position) < backAwayDistance && _gameManager.RobotFollow)
            {
                return true;
            }
            else
            {
                return false;
            }
        });

        // Create an Inverter node to invert the result of the _followCondition
        invertedFollowCondition = new Inverter(_followCondition);
        // Create an Inverter node to invert the result of the _followDistanceCondition
        invertedFollowDistanceCondition = new Inverter(_followDistanceCondition);

        // Create root node with a Selector node and two Sequence nodes
        root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                invertedFollowCondition,
                new ReturnToLobby(agent, _gameManager.LobbySpawn.transform),
            }),
            new Sequence(new List<Node>
            {
                _backAwayCondition,
                _backAwayNode,
            }),
            new Sequence(new List<Node>
            {
                _followCondition,
                _followDistanceCondition,
                new FollowPlayer(transform, _gameManager.PlayerObject.transform, agent, followDistance),
            }),
            new Sequence(new List<Node>
            {
                invertedFollowDistanceCondition,
                new Idle(agent),
            }),
        });

        _root = root;
    }

    protected override void Update()
    {
        base.Update();

        // Update player data on each tick
        _root.SetData((string)GameManager.Instance.PlayerObject.name, _gameManager.PlayerObject.transform);

        // Check if shouldFollow value has changed in the GameManager during runtime
        if (_followCondition != null && _followCondition.IsConditionMet() != _gameManager.RobotFollow)
        {
            // Recreate the behavior tree if shouldFollow value has changed
            RecreateBehaviorTree(GetComponent<NavMeshAgent>());
        }

        // Execute the behavior tree
        _root.Evaluate();
    }

    private void RecreateBehaviorTree(NavMeshAgent agent)
    {
        var player = _gameManager.PlayerObject;
        var robot = _gameManager.RoboterObject;
        var lobbySpawn = _gameManager.LobbySpawn;

        // Clear the children of the root node
        root.children.Clear();

        // Recreate the follow and back away conditions
        _followCondition = new CustomCondition(() => _gameManager.RobotFollow);

        _followDistanceCondition = new CustomCondition(() =>
        {
            return Vector3.Distance(player.transform.position, robot.transform.position) > followDistance;
        });

        // Recreate inverted conditions
        invertedFollowCondition = new Inverter(_followCondition);
        invertedFollowDistanceCondition = new Inverter(_followDistanceCondition);

        // Create a Sequence node to contain the FollowPlayer node
        var followSequence = new Sequence(new List<Node>
        {
            new FollowPlayer(transform, _gameManager.PlayerObject.transform, agent, followDistance)
        });

            // Create the root node with a Selector node and the re-ordered Sequence nodes
            root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    invertedFollowCondition,
                    new ReturnToLobby(agent, lobbySpawn.transform),
                }),
                new Sequence(new List<Node>
                {
                    _backAwayCondition,
                    _backAwayNode,
                }),
                new Sequence(new List<Node>
                {
                    _followCondition,
                    _followDistanceCondition,
                    followSequence,
                }),
                new Sequence(new List<Node>
                {
                    invertedFollowDistanceCondition,
                    new Idle(agent),
                }),
            });

        // Set the root node
        _root = root;
    }


    public void TeleportPlayer()
    {
        var player = _gameManager.PlayerObject;
        //Debug.Log("Teleport");
        player.transform.position = _gameManager.LobbySpawn.transform.position;
    }

    public void ToggleFollowPlayer()
    {
        //Debug.Log("Follow");
        _gameManager.RobotFollow = !_gameManager.RobotFollow; // Toggle the value in the GameManager
    }
}
