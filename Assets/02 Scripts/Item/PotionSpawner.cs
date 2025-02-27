using UnityEngine;

public class PotionSpawner : ItemSpawner
{
    public GameObject[] itemList;  //������ ����Ʈ

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

    //������ ������ �������� ����
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
