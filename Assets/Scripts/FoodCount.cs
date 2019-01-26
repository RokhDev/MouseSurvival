using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCount : MonoBehaviour
{
    GameObject[] findCheese;
    int foodFound;
    int loopFound;

    void Start()
    {
        findCheese = GameObject.FindGameObjectsWithTag("Cheese");
        loopFound = 0;
        for (int i = 0; i < findCheese.Length; i++)
        {
            if (findCheese[i].activeSelf)
            {
                loopFound++;
            }
        }
        foodFound = loopFound;
    }

    void Update()
    {
        RefreshCount();
        CheckVictory();
    }

    void RefreshCount()
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
    }
    void CheckVictory()
    {
        if (foodFound <= 0)
        {
            Debug.Log("Victory");
        }
    }
}
