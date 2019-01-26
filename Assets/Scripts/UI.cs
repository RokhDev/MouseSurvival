using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Canvas canvas;
    GameObject home;
    GameObject mice;
    GameObject foodCount;
    [SerializeField]
    public Text Score;
    [SerializeField]
    public Text Carrying;
    // Text myText;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        home = GameObject.FindGameObjectWithTag("Home");
        // mice = GameObject.FindGameObjectWithTag("Player");
        foodCount = GameObject.FindGameObjectWithTag("FoodCount");
    }

    void Update()
    {
        if (!mice)
        {
            mice = Globals.player;
        }
        Score.text = "Score " + home.GetComponent<Home>().GetScore() + " / " + foodCount.GetComponent<FoodCount>().GetFoodFound(); 
        Score.color = Color.blue;
        Carrying.text = "Carrying " + mice.GetComponent<Mice>().GetFoodCount();
        Carrying.color = Color.red;
    }
}
