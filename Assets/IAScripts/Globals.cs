using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

    public static GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Mice");
    }
}
