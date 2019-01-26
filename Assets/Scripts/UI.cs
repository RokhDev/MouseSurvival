using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Canvas canvas;
    [SerializeField]
    public Text Score;
    [SerializeField]
    public Text Carrying;
    // Text myText;
    private Home homeBh;
    private FoodCount foodCountBh;
    private Mice miceBh;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        homeBh = GameObject.FindGameObjectWithTag("Home").GetComponent<Home>();
        // mice = GameObject.FindGameObjectWithTag("Player");
        foodCountBh = GameObject.FindGameObjectWithTag("FoodCount").GetComponent<FoodCount>();
    }

    void Update()
    {
        if (!miceBh)
        {
            miceBh = Globals.player.GetComponent<Mice>();
        }
        Score.text = "Carrying " +  miceBh.GetFoodCount() + " / " + foodCountBh.GetFoodFound(); 
        Score.color = Color.blue;
        Carrying.text = "Score " + homeBh.GetScore();
        Carrying.color = Color.red;
    }
}
