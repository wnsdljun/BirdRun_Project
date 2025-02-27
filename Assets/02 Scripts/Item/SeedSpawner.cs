using UnityEngine;

public class SeedSpawner : ItemSpawner
{

    public GameObject[] itemList;  //아이템 리스트

    public float sunSeedChance = 0.7f;  // 일반 씨앗 확률 (70%)
    public float rainbowSeedChance = 0.2f; // 무지개 씨앗 확률 (20%)
    public float FruitChance = 0.1f;   // 과일 아이템 확률 (10%)

    //아이템 생성
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

    //확률에 맞게 아이템 랜덤 결정
    public override GameObject SelectRandomItem()
    {
        float randomValue = Random.value;

        if (GameManager.Instance.isSpeedUp)
        {
            return itemList[1];
        }
        else
        {
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
}
