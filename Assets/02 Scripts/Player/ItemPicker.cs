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
        if (item == null) yield break; //아이템이 삭제되어 없다면

        //처음 위치에서 플레이어 위치까지 거리 계산 후 이동속도 산출
        Vector2 initialPos = item.transform.position;
        Vector2 distance = (Vector2)player.transform.position - initialPos;
        float moveSpeed = distance.magnitude / pickupDuration / Time.fixedDeltaTime;
        //물리연산의 dT를 코루틴 간격으로 사용.

        Vector2 heading;
        while (item != null)
        {
            heading = (Vector2)(player.transform.position - item.transform.position).normalized;
            item.transform.position += (Vector3)(heading * moveSpeed);

            yield return new WaitForFixedUpdate();
        }
    }
}
