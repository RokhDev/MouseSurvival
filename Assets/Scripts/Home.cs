using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    int score;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        
    }

    public void SumScore(int sum)
    {
        score += sum;
    }

    public void SetScore(int points)
    {
        score = points;
    }

    public int GetScore()
    {
        return score;
    }
}
