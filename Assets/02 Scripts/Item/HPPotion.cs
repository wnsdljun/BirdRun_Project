using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPotion : MonoBehaviour, IItemEffect
{
    float heal;

    private void Awake()
    {
        if (transform.name.Contains("Small"))
            heal = 10;
        else if (transform.name.Contains("Large"))
            heal = 50;
    }

    public void ApplyEffect(Player player)
    {
        player.Heal(heal);
    }
}