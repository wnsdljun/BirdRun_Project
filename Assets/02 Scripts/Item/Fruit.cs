using UnityEngine;

public class Fruit : MonoBehaviour, IItemEffect
{
    public void ApplyEffect(Player player)
    {
        GameManager.Instance.AddFruitCount();
    }
}
