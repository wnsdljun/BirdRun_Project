using UnityEngine;

public class SpeedUp : MonoBehaviour, IItemEffect
{
    public float speed = 10f;
    public float duration = 5f;

    public void ApplyEffect(Player player)
    {
        player.SpeedUp(speed, duration);
    }
}
