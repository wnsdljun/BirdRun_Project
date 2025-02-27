using UnityEngine;

public class SeedSpawner : ItemSpawner
{

    public GameObject[] itemList;  //������ ����Ʈ

    public float sunSeedChance = 0.7f;  // �Ϲ� ���� Ȯ�� (70%)
    public float rainbowSeedChance = 0.2f; // ������ ���� Ȯ�� (20%)
    public float FruitChance = 0.1f;   // ���� ������ Ȯ�� (10%)

    //������ ����
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

    //Ȯ���� �°� ������ ���� ����
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
