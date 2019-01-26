using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Waypoint pf = (Waypoint)this.target;

        if (GUILayout.Button("Generate Links"))
        {
            Waypoint.GenerateLinks();
        }

        if (GUILayout.Button("Clear Links"))
        {
            Waypoint.ClearLinks();
        }
    }
}
