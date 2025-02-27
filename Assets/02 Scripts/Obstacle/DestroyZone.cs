using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour

    //ground와 부딪히면 ground 삭제
{    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Ground"))
            {
                Destroy(collision.transform.parent.gameObject);
            }
            else if (collision.CompareTag("BackGround"))
            {
                float widthOfBgObject = ((BoxCollider2D)collision).size.x;
                Vector3 pos = collision.transform.position;

                pos.x += widthOfBgObject * 2;

                collision.transform.position = pos;
            }
        }
    }
}
