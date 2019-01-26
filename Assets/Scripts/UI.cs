using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    Canvas canvas;
    GameObject home;
    //Text myText;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        home = GameObject.FindGameObjectWithTag("Home");
    }

    void Update()
    {
        canvas.GetComponentInChildren<Text>().text = "Score " + home.GetComponent<Home>().GetScore();
        canvas.GetComponentInChildren<Text>().color = Color.blue;
    }
}
