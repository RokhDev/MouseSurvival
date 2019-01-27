using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

    public AudioClip sound1;

    public static AudioClip sound1S;
    public static GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Mice");
        sound1S = sound1;
    }
}
