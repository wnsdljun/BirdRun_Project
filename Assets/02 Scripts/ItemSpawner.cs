using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPoint;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("ItemPoint"))
            {
                itemPoint.Add(child.gameObject);
            }
        }
    }

    public abstract void SpawnItem();

    public abstract GameObject SelectRandomItem();
}