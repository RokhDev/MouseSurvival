using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodCount : MonoBehaviour
{
    GameObject[] findCheese;
    GameObject findHome;
    int foodFound;
    // int loopFound;

    void Start()
    {
        findCheese = GameObject.FindGameObjectsWithTag("Cheese");
        findHome = GameObject.FindGameObjectWithTag("Home");
        // loopFound = 0;
        /*for (int i = 0; i < findCheese.Length; i++)
        {
            if (findCheese[i].activeSelf)
            {
                loopFound++;
            }
        }*/
        foodFound = findCheese.Length;
    }

    void Update()
    {
        // RefreshCount();
        CheckVictory();
    }

    /*void RefreshCount()
    {
        loopFound = 0;
        for (int i = 0; i < findCheese.Length; i++)
        {
            if (findCheese[i].activeSelf)
            {
                loopFound++;
            }
        }
        foodFound = loopFound;
    }*/
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
