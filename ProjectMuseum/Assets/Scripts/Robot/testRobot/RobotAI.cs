using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{

    private Transform player;
    [SerializeField] public bool followPlayer { get; set; } = true;
    [SerializeField, Range(5, 20)] private float stopDistance = 5f; // The distance at which the robot will stop following the player
    [SerializeField, Range(1, 4.99f)] private float backAwayDistance = 3f; // The distance at which the robot will start backing away from the player
    private NavMeshAgent agent;
    private Vector3 originalPosition; // The original position of the robot (to move to when followPlayer is set to false)



    // Start is called before the first frame update
    void Start()
    {
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
        if (followPlayer)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            Debug.Log("Distance: " + distance);

            if (distance > backAwayDistance && distance < stopDistance)
            {
                agent.SetDestination(transform.position);

                return;
            }

            else if (distance <= backAwayDistance)
            {
                Debug.Log("distance <= backAwayDistance: " + distance);
                // Calculate the direction to move away from the player
                Vector3 awayDirection = (transform.position - player.position);
                //Vector3 awayDirection = (transform.position - player.position).normalized; ????????
                Vector3 backAwayPosition = transform.position + awayDirection * backAwayDistance;

                // Sample a valid position on the NavMesh for back away
                NavMeshHit navMeshHit;
                NavMesh.SamplePosition(backAwayPosition, out navMeshHit, backAwayDistance, NavMesh.AllAreas);
                //Debug.Log(navMeshHit.hit);
                if (navMeshHit.hit)
                {
                    Debug.Log("NavMesh position set");
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
            // If followPlayer is false, return to the original position
            agent.updateRotation = true;
            agent.isStopped = false;
            agent.SetDestination(originalPosition);

            return;
        }
    }

}
