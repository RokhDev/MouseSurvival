using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodCount : MonoBehaviour
{
    GameObject[] findCheese;
    GameObject findHome;
    int foodFound;

    void Start()
    {
        findCheese = GameObject.FindGameObjectsWithTag("Cheese");
        findHome = GameObject.FindGameObjectWithTag("Home");
        foodFound = findCheese.Length;
    }

    void Update()
    {
        CheckVictory();
    }

    public int GetFoodFound()
    {
        return foodFound;
    }

    void CheckVictory()
    {
        if (findHome.GetComponent<Home>().GetScore() >= foodFound)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
