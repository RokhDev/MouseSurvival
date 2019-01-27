using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public Sprite[] foodGraphics;

    private SpriteRenderer r;
    //AudioSource source;

    void Start()
    {
        r = GetComponent<Renderer>() as SpriteRenderer;
        int picker = Random.Range(0, foodGraphics.Length);
        r.sprite = foodGraphics[picker];
        //source = GetComponent<AudioSource>();
    }
}
