using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] bool selectable;
    [SerializeField] float flipDuration = 0.5f;
    Vector2 originPos;
    bool isFlip = false;
    private void Start()
    {
        originPos = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Card card = hit.collider.GetComponent<Card>();
                if (card == this)
                {
                    if (card.selectable)
                    {
                        card.ClickCard();
                    }
                    else
                    {
                        card.StartCoroutine(UnavailableCardClick());
                    }
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
            yield return null;
        }
        yield return null;
    }

    IEnumerator UnavailableCardClick()
    {
        float time = 0.5f;
        float elapsed = 0f;
        float range = 0.3f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            transform.position = new Vector2(Random.Range(-range, range), 0) + originPos;
            if (!(elapsed < time))
            {
                transform.position = originPos;
            }
            yield return null;
        }
    }
}
