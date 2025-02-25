using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OhterItemSpawner : ItemSpawner
{
    public GameObject[] itemList;  //아이템 리스트

    public override void SpawnItem()
    {
        foreach (var itemPosition in itemPoint)
        {
            Vector3 newPosition = itemPosition.transform.position;

            GameObject randomItem = SelectRandomItem();
            GameObject newItem = Instantiate(randomItem, newPosition, Quaternion.identity);

            newItem.transform.SetParent(transform);
        }
    }

    public override GameObject SelectRandomItem()
    {
        int randomIdx = Random.Range(0, itemList.Length);
        
        return itemList[randomIdx];
    }
}
