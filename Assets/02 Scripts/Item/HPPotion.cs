using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPotion : MonoBehaviour, IItemEffect
{
    float healPercent;

    private void Awake()
    {
        if (transform.name.Contains("Small"))
            healPercent = 0.25f;
        else if (transform.name.Contains("Large"))
            healPercent = 0.5f;
    }

    public void ApplyEffect(Player player)
    {
        player.Heal(healPercent);
    }
}