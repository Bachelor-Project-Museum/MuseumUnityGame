using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float stopDistance = 5f; // The distance at which the robot will stop following the player
    [SerializeField] private float backAwayDistance = 3f; // The distance at which the robot will start backing away from the player
    [SerializeField] private GameObject lobbySpawn;

    private NavMeshAgent agent;
    private Vector3 originalPosition; // The original position of the robot (to move to when RobotFollow is set to false)

    private GameManager _gameManager; // Reference to the GameManager script

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        player = GameManager.Instance.PlayerObject.transform;

        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBehavior();
    }

    void UpdateBehavior()
    {
        if (_gameManager.RobotFollow)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            //Debug.Log("Distance: " + distance);

            if (distance > backAwayDistance && distance < stopDistance)
            {
                agent.SetDestination(transform.position);
                return;
            }
            else if (distance <= backAwayDistance)
            {
                Vector3 awayDirection = (transform.position - player.position).normalized;
                Vector3 backAwayPosition = transform.position + awayDirection * backAwayDistance;

                // Sample a valid position on the NavMesh for back away
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(backAwayPosition, out navMeshHit, backAwayDistance, NavMesh.AllAreas))
                {
                    backAwayPosition = navMeshHit.position;
                    // Move towards the backAwayPosition
                    agent.updateRotation = false;
                    agent.SetDestination(backAwayPosition);
                }

                return;
            }
            else if (distance > stopDistance)
            {
                agent.updateRotation = true;
                agent.isStopped = false;
                agent.SetDestination(player.position);
                return;
            }
            else if (distance <= stopDistance)
            {
                agent.SetDestination(transform.position);
                return;
            }
        }
        else
        {
            // If RobotFollow is false, return to the original position
            agent.updateRotation = true;
            agent.isStopped = false;
            agent.SetDestination(lobbySpawn.transform.position);
            return;
        }
    }

    public void TeleportPlayer()
    {
        //Debug.Log("Teleport");
        GameManager.Instance.PlayerObject.transform.position = lobbySpawn.transform.position;
    }

    public void ToggleFollowPlayer()
    {
        //Debug.Log("Follow");
        _gameManager.RobotFollow = !_gameManager.RobotFollow; // Toggle the value in the GameManager
    }
}
