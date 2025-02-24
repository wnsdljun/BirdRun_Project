using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] seedItemList;   //���� ����Ʈ
    public GameObject[] itemList;  //������ ����Ʈ
    public List<GameObject> itemPoint;

    public float sunSeedChance = 0.7f;  // �Ϲ� ���� Ȯ�� (70%)
    public float rainbowSeedChance = 0.2f; // ������ ���� Ȯ�� (20%)
    public float otherItemChance = 0.1f;   // �ٸ� ������ Ȯ�� (10%)

    private void Awake()
    {
        itemPoint.Add(transform.Find("ItemPoint1").gameObject);
        itemPoint.Add(transform.Find("ItemPoint2").gameObject);
    }

    public void SpawnItem()
    {
        foreach (var itemPosition in itemPoint)
        {
            Vector3 newPosition = itemPosition.transform.position;

            GameObject randomItem = SelectRandomItem();
            GameObject newItem = Instantiate(randomItem, newPosition, Quaternion.identity);

            newItem.transform.SetParent(transform);
        }
    }

    GameObject SelectRandomItem()
    {
        float randomValue = Random.value;

        if (randomValue < sunSeedChance)
            return seedItemList[0];
        else if (randomValue < rainbowSeedChance)
            return seedItemList[1];
        else
            return itemList[Random.Range(0, itemList.Length)];
    }
}