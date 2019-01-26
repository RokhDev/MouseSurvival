using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float chasingSpeed;
    public float idleTime;
    public float wpProximityTreshold;

    private States state;
    private Waypoint currentTgtWaypoint;
    private float idleTimer = 0;

    private void Start()
    {
        currentTgtWaypoint = FindClosest();
    }

    private void Update()
    {
        if(state == States.Patroling)
        {
            Vector3 prevPos = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, currentTgtWaypoint.transform.position, patrolSpeed * Time.deltaTime);
            Vector3 direction = transform.position - prevPos;
            direction.Normalize();

            float distanceToTgtWp = Vector3.Distance(transform.position, currentTgtWaypoint.transform.position);
            if(distanceToTgtWp <= wpProximityTreshold)
            {
                state = States.Idle;
                idleTimer = idleTime;
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
        }
        else if(state == States.Chasing)
        {

        }
        else if(state == States.Searching)
        {

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

    private bool MouseOnSight()
    {
        return true;
    }
}
