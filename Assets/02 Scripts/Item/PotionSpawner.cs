using UnityEngine;

public class PotionSpawner : ItemSpawner
{
    public GameObject[] itemList;  //아이템 리스트

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

    //아이템 종류를 랜덤으로 결정
    public override GameObject SelectRandomItem()
    {
        if (GameManager.Instance.potionType == "Small")
            return itemList[0];
        else if (GameManager.Instance.potionType == "Large")
            return itemList[1];
        else
            return null;
    }
}
