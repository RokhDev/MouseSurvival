using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    
    public Waypoint[] adjacents;
    public LayerMask layerMask;

    public static void GenerateLinks()
    {
        GameObject[] goWaypoint = GameObject.FindGameObjectsWithTag("Waypoint");
        Stack<GameObject> adjacentWaypoints = new Stack<GameObject>();

        for(int i = 0; i < goWaypoint.Length; i++)
        {
            adjacentWaypoints.Clear();
            Waypoint wpInfo = goWaypoint[i].GetComponent<Waypoint>();
            for(int j = 0; j < goWaypoint.Length; j++)
            {
                if(i != j)
                {
                    RaycastHit2D[] hit = Physics2D.LinecastAll(goWaypoint[i].transform.position, goWaypoint[j].transform.position, wpInfo.layerMask);
                    Debug.DrawRay(goWaypoint[i].transform.position, goWaypoint[j].transform.position - goWaypoint[i].transform.position, Color.red, 1.0f);
                    if (hit.Length == 0)
                    {
                        adjacentWaypoints.Push(goWaypoint[j]);
                    }
                }
            }
            Waypoint wp = goWaypoint[i].GetComponent<Waypoint>();
            wp.adjacents = new Waypoint[adjacentWaypoints.Count];
            for(int k = 0; k < wp.adjacents.Length; k++)
            {
                wp.adjacents[k] = adjacentWaypoints.Pop().GetComponent<Waypoint>();
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(wp);
#endif
        }
    }

    public static void ClearLinks()
    {
        GameObject[] goWaypoint = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach(GameObject wp in goWaypoint)
        {
            wp.GetComponent<Waypoint>().adjacents = new Waypoint[0];
        }
    }
}
