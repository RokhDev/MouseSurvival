using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    public Text score;
    [SerializeField]
    public Text carrying;
    private Home homeBh;
    private FoodCount foodCountBh;
    private Mice miceBh;

    void Start()
    {
        homeBh = GameObject.FindGameObjectWithTag("Home").GetComponent<Home>();
        foodCountBh = GameObject.FindGameObjectWithTag("FoodCount").GetComponent<FoodCount>();
    }

    void Update()
    {
        CheckMice();
        ShowScore();
        ShowFoodCarrying();
    }

    void CheckMice()
    {
        if (!miceBh)
        {
            miceBh = Globals.player.GetComponent<Mice>();
        }
    }

    void ShowScore()
    {
        score.text = "Score " + homeBh.GetScore() + " / " + foodCountBh.GetFoodFound();
        score.color = Color.blue;
    }

    void ShowFoodCarrying()
    {
        carrying.text = "Carrying " + miceBh.GetFoodCount();
        carrying.color = Color.red;
    }
}
