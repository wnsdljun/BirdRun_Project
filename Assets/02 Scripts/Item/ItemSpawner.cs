using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPoint;

    private void Awake()
    {
        //아이템이 생성 될 위치 지정
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