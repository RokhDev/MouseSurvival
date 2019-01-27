using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    int score;
    AudioSource source;

    void Start()
    {
        score = 0;
        source = GetComponent<AudioSource>();
    }

    public void SumScore(int sum)
    {
        score += sum;
        source.Play();
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
