using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour, IItemEffect
{
    int score;

    private void Awake()
    {
        if (transform.name.Contains("Sun"))
            score = 10;
        else if (transform.name.Contains("Rainbow"))
            score = 50;
    }

    public void ApplyEffect(Player player)
    {

    }
}
