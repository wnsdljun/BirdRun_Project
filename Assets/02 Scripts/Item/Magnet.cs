using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour, IItemEffect
{
    public float duration = 5f;

    public void ApplyEffect(Player player)
    {
        player.UseMagnet(duration);
    }
}
