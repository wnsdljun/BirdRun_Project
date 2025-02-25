using System.Collections;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] private float pickupDuration = 0.5f;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is IItemEffect item)
        {
            //item.ApplyEffect(player.ToString());

        }
        if (collision.CompareTag("Item"))
        {
            StartCoroutine(PickupItem(collision.gameObject));
        }
    }

    public IEnumerator PickupItem(GameObject item)
    {
        if (item == null) yield break; //�������� �����Ǿ� ���ٸ�

        //ó�� ��ġ���� �÷��̾� ��ġ���� �Ÿ� ��� �� �̵��ӵ� ����
        Vector2 initialPos = item.transform.position;
        Vector2 distance = (Vector2)player.transform.position - initialPos;
        float moveSpeed = distance.magnitude / pickupDuration / Time.fixedDeltaTime;
        //���������� dT�� �ڷ�ƾ �������� ���.

        Vector2 heading;
        while (item != null)
        {
            heading = (Vector2)(player.transform.position - item.transform.position).normalized;
            item.transform.position += (Vector3)(heading * moveSpeed);

            yield return new WaitForFixedUpdate();
        }
    }
}
