using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] float flipDuration = 0.5f;
    Button button;

    bool isFlip = false;
    bool canClick = true;
    private void Update()
    {
        if (canClick && Input.GetMouseButtonUp(0))
        {
            canClick = false;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Card card = hit.collider.GetComponent<Card>();
                if (card != null)
                {
                    card.ClickCard();
                    return;
                }
            }
        }
    }

    void ClickCard()
    {
        if (!isFlip)
        {
            isFlip = true;
            StartCoroutine(FlipCoroutine());
            return;
        }
        else
        {
            GameManager.Instance.SelectedCharater = character;
            GameManager.Instance.LoadScene("04 GameScene");
        }
    }

    IEnumerator FlipCoroutine()
    {
        float elapsed = 0;
        float rotY;
        while (elapsed < flipDuration)
        {
            rotY = Mathf.Lerp(0f, 180f, elapsed / flipDuration);
            transform.rotation = Quaternion.Euler(0, rotY, 0);
            elapsed += Time.deltaTime;
            canClick = !(elapsed < flipDuration);
            yield return null;
        }
    }
}
