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

            float distanceToTgtWp = Vector3.Distance(transform.position, currentTgtWaypoint.transform.position);
            if(distanceToTgtWp <= wpProximityTreshold)
            {
                state = States.Idle;
                idleTimer = idleTime;
            }

            if (PlayerInSight())
            {
                state = States.Chasing;
                chasingSpeed = patrolSpeed;
            }

        }
        else if(state == States.Idle)
        {
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

            if(Vector3.Distance(transform.position, playerLastPosition) > searchProximityTreshold)
            {
                MoveTowardsPlayerLastPosition();
            }
            else
            {
                searchTimer -= Time.deltaTime;
                if(searchTimer <= 0)
                {
                    currentTgtWaypoint = FindClosest();
                    state = States.Patroling;
                }
            }

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

        return true;
    }

    private void MoveTowardsWaypoint()
    {
        prevPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentTgtWaypoint.transform.position, patrolSpeed * Time.deltaTime);
        direction = transform.position - prevPos;
        direction.Normalize();
    }

    private void MoveTowardsPlayer()
    {
        prevPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chasingSpeed * Time.deltaTime);
        direction = transform.position - prevPos;
        direction.Normalize();
        playerLastPosition = player.transform.position;
    }

    private void MoveTowardsPlayerLastPosition()
    {
        prevPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, playerLastPosition, chasingSpeed * Time.deltaTime);
        direction = transform.position - prevPos;
        direction.Normalize();
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