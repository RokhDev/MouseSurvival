using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour {
    
    public enum States
    {
        Patroling,
        Idle,
        Chasing,
        Searching
    };

    public LayerMask mouseLayer;
    public LayerMask obstacleLayer;
    public float patrolSpeed;
    public float maxSpeed;
    public float acceleration;
    public float idleTime;
    public float searchTimeout;
    public float viewDistance;
    public float wpProximityTreshold;
    public float searchProximityTreshold;
    public float playerProximityTreshold;

    Animator anim;
    [SerializeField]
    RuntimeAnimatorController front;
    [SerializeField]
    RuntimeAnimatorController back;
    [SerializeField]
    RuntimeAnimatorController left;
    [SerializeField]
    RuntimeAnimatorController right;

    private States state;
    private float chasingSpeed;
    private Waypoint currentTgtWaypoint;
    private float idleTimer = 0;
    private float searchTimer = 0;
    private GameObject player;
    private Vector3 prevPos;
    private Vector3 direction;
    private Vector3 playerLastPosition;

    private void Start()
    {
        currentTgtWaypoint = FindClosest();
        chasingSpeed = patrolSpeed;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!player)
        {
            player = Globals.player;
        }

        if(state == States.Patroling)
        {
            MoveTowardsWaypoint();

            //Cambio de Patroling a Idle
            float distanceToTgtWp = Vector3.Distance(transform.position, currentTgtWaypoint.transform.position);
            if(distanceToTgtWp <= wpProximityTreshold)
            {
                state = States.Idle;
                idleTimer = idleTime;
            }

            //Cambio de Patroling a Chasing
            if (PlayerInSight())
            {
                state = States.Chasing;
                chasingSpeed = patrolSpeed;
            }

        }
        else if(state == States.Idle)
        {
            if (anim.enabled)
                anim.enabled = false;

            //Cambio de Idle a Patroling
            if(idleTimer > 0)
            {
                idleTimer -= Time.deltaTime;
            }
            else
            {
                int picker = Random.Range(0, currentTgtWaypoint.adjacents.Length);
                currentTgtWaypoint = currentTgtWaypoint.adjacents[picker];
                state = States.Patroling;
            }

            //Cambio de Idle a Chasing
            if (PlayerInSight())
            {
                state = States.Chasing;
                chasingSpeed = patrolSpeed;
            }
        }
        else if(state == States.Chasing)
        {
            chasingSpeed += acceleration * Time.deltaTime;

            CheckPlayerProximity();

            //Cambio de Chasing a Searching
            if (PlayerInSight())
            {
                if(Vector3.Distance(transform.position, player.transform.position) > playerProximityTreshold)
                    MoveTowardsPlayer();
            }
            else
            {
                searchTimer = searchTimeout;
                state = States.Searching;
            }
        }
        else if(state == States.Searching)
        {
            chasingSpeed += acceleration * Time.deltaTime;

            //Cambio de Searching a Patroling
            if(Vector3.Distance(transform.position, playerLastPosition) > searchProximityTreshold)
            {
                MoveTowardsPlayerLastPosition();
            }
            else
            {
                if (anim.enabled)
                    anim.enabled = false;
                searchTimer -= Time.deltaTime;
                if(searchTimer <= 0)
                {
                    currentTgtWaypoint = FindClosest();
                    state = States.Patroling;
                }
            }

            //Cambio de Searching a Chasing
            if (PlayerInSight())
            {
                state = States.Chasing;
            }
        }
    }

    private Waypoint FindClosest()
    {
        GameObject[] goWaypoint = GameObject.FindGameObjectsWithTag("Waypoint");
        List<Waypoint> validWaypoints = new List<Waypoint>(); 

        for (int i = 0; i < goWaypoint.Length; i++)
        {
            RaycastHit2D[] hit = Physics2D.LinecastAll(transform.position, goWaypoint[i].transform.position, obstacleLayer);
            if (hit.Length == 0)
            {
                validWaypoints.Add(goWaypoint[i].GetComponent<Waypoint>());
            }
        }

        Waypoint closest = validWaypoints[0];
        if (validWaypoints.Count <= 1)
        {
            return closest;
        }
        float closestDistance = Vector3.Distance(transform.position, closest.transform.position);

        for(int i = 1; i < validWaypoints.Count; i++)
        {
            float wpDistance = Vector3.Distance(transform.position, validWaypoints[i].transform.position);
            if(wpDistance < closestDistance)
            {
                closest = validWaypoints[i];
                closestDistance = wpDistance;
            }
        }
        return closest;
    }

    private bool PlayerInSight()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > viewDistance)
            return false;

        RaycastHit2D[] hit = Physics2D.LinecastAll(transform.position, player.transform.position, obstacleLayer);
        if (hit.Length > 0)
            return false;

        playerLastPosition = player.transform.position;
        return true;
    }

    private void MoveTowardsWaypoint()
    {
        prevPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentTgtWaypoint.transform.position, patrolSpeed * Time.deltaTime);
        direction = currentTgtWaypoint.transform.position;
        UpdateAnimationSheet();
    }

    private void MoveTowardsPlayer()
    {
        prevPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chasingSpeed * Time.deltaTime);
        direction = player.transform.position;
        UpdateAnimationSheet();
    }

    private void MoveTowardsPlayerLastPosition()
    {
        prevPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, playerLastPosition, chasingSpeed * Time.deltaTime);
        direction = playerLastPosition;
        UpdateAnimationSheet();
    }

    private void UpdateAnimationSheet()
    {
        if (!anim.enabled)
            anim.enabled = true;
        Vector3 quadrant = Magic.MathS.GetQuadrantToDirection(prevPos, direction);
        if(quadrant == Vector3.right)
        {
            anim.runtimeAnimatorController = right;
        }
        if (quadrant == Vector3.left)
        {
            anim.runtimeAnimatorController = left;
        }
        if (quadrant == Vector3.up)
        {
            anim.runtimeAnimatorController = back;
        }
        if (quadrant == Vector3.down)
        {
            anim.runtimeAnimatorController = front;
        }
    }

    private void CheckPlayerProximity()
    {
        float proximity = Vector3.Distance(transform.position, player.transform.position);
        if (proximity <= playerProximityTreshold)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    private bool MouseOnSight()
    {
        return true;
    }
}