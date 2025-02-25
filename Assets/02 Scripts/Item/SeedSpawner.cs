using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : ItemSpawner
{
    public GameObject[] itemList;  //¾ÆÀÌÅÛ ¸®½ºÆ®

    public float sunSeedChance = 0.7f;  // ÀÏ¹Ý ¾¾¾Ñ È®·ü (70%)
    public float rainbowSeedChance = 0.2f; // ¹«Áö°³ ¾¾¾Ñ È®·ü (20%)
    public float FruitChance = 0.1f;   // °úÀÏ ¾ÆÀÌÅÛ È®·ü (10%)

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
        float randomValue = Random.value;

        if (randomValue < sunSeedChance)
            return itemList[0];
        else if (randomValue < sunSeedChance + rainbowSeedChance)
            return itemList[1];
        else if (randomValue < sunSeedChance + rainbowSeedChance + FruitChance)
            return itemList[2];
        else
            return null;
    }
}
