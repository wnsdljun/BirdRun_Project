using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Animator _animator;
    BoxCollider2D _boxCollider;

    [SerializeField] private float moveSpeed = 3f;

    public float AddMoveSpeed
    {
        set
        {
            moveSpeed += value;
        }
    }
    [SerializeField] private float collisionPenalty = 5f; //�浹 �� ���ҽ�ų �ð�

    #region ���� ���� ����
    //Update���� �Է��� �ް� 
    [SerializeField] private float jumpPower = 20f;

    private bool isOnGround = false;
    private bool isDoubleJumped = false;
    private void Jump()
    {
        Debug.Log("���� ȣ���.");
        if (isDoubleJumped) return; //2�������� �ߴٸ� ���ư���.
        if (isOnGround) isOnGround = false;
        else if (!isOnGround && !isDoubleJumped) isDoubleJumped = true;//���� �پ����� �ʴٸ� ���� �ӵ� 0
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    #endregion
    #region �����̵� ���� ����
    private bool isSliding = false;
    private Vector2 colliderOffsetAtStand;
    private Vector2 colliderSizeAtStand;
    private Vector2 colliderOffsetAtSlide;
    private Vector2 colliderSizeAtSlide;
    //�����̵� ���¸� �����ϸ� �ִϸ����͵� �� �ٲ���.
    public bool IsSliding
    {
        get => isSliding;
        set
        {
            if (isSliding != value)
            {
                isSliding = value;
                Slide();
            }

        }
    }

    private void Slide()
    {
        _animator.SetBool("IsSlide", isSliding);
        if (isSliding) //�����̵����̶��
        {
            _boxCollider.size = colliderSizeAtSlide;
            _boxCollider.offset = colliderOffsetAtSlide;
        }
        else
        {
            _boxCollider.size = colliderSizeAtStand;
            _boxCollider.offset = colliderOffsetAtStand;
        }
    }
    #endregion
    #region ��Ʈ ���� ������ ���⿡
    [SerializeField] private float initialSurviveTime;//���� �ð�- �����Ϳ��� ����
    private float surviveTime = 0f; //�� ���� �����ϸ� �ʱ� ���� �ð� ������ ũ�ų� ���ԵǸ� ���ӿ���
    public float SurviveTime
    {
        get => surviveTime;
        set
        {
            //��Ʈ ������ ���� �Ծ �����ð��� ������ �������� ���?
            surviveTime = value;
            if (surviveTime >= initialSurviveTime) TimeOver();
        }
    }

    private void TimeOver()
    {
        Debug.Log("�ð� �� �ƴ�");
        Time.timeScale = 0;
    }
    #endregion
    #region ������ ��� ������ ���⿡
    public void Heal(float amount)
    {
        SurviveTime -= SurviveTime * amount;
        Debug.Log($"�����ð� \"{amount}\" ȸ��. total: {SurviveTime}");
    }
    private float speedBoost = 0f;
    private float speedBoostDuration = 0f;
    public void SpeedUp(float speed, float duration)
    {
        speedBoost = speed;
        speedBoostDuration = duration;
        Debug.Log($"���ǵ�� for [{duration}]");
    }
    public void UseMagnet(float duration)
    {

    }
    private IEnumerator ApplyMagnetEffect()
    {

        return null;
    }
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        #region �����̵� �Ҷ� �ݶ��̴� ũ�� �ٲ��ִ� �κ�. ũ�⸦ �ҷ��� �����Ѵ�.
        _boxCollider = GetComponent<BoxCollider2D>();
        colliderOffsetAtStand = _boxCollider.offset;
        colliderSizeAtStand = _boxCollider.size;
        colliderOffsetAtSlide = new Vector2(_boxCollider.offset.x, 0f);
        colliderSizeAtSlide = new Vector2(_boxCollider.size.x, 0.9f);
        #endregion 
    }


    // Update is called once per frame
    void Update()
    {
        //����
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        //�����̵�
        if (isOnGround && Input.GetKeyDown(KeyCode.LeftShift))
            IsSliding = true;
        if (isSliding && Input.GetKeyUp(KeyCode.LeftShift))
            IsSliding = false;
    }

    private void FixedUpdate()
    {
        //�̵��ӵ� ����
        _rigidbody.velocity = new Vector2(moveSpeed + speedBoost, _rigidbody.velocity.y);
        if (speedBoostDuration >= 0)
        {
            GameManager.Instance.isSpeedUp = true;
            speedBoostDuration -= Time.fixedDeltaTime;
        }
        else
        {
            GameManager.Instance.isSpeedUp = false;
            speedBoost = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //����� �浹�� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isDoubleJumped = false;
            isOnGround = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�������� �÷��̾�� ����� �浹�ߴٸ�
        if (collision.CompareTag("Item"))
        {
            IItemEffect item = collision.gameObject.GetComponent<IItemEffect>();
            item.ApplyEffect(this);
            Destroy(collision.gameObject);
        }
        //��ֹ� �浹�� ����
        if (collision.CompareTag("Obstacle"))
        {
            if (speedBoostDuration <= 0) //�������� �ƴϸ�
            {
                Debug.Log($"��ֹ� �浹, �����ð� ���� ���� {SurviveTime}");
                SurviveTime += collisionPenalty;
            }
            else //�������϶�
            {
                Debug.Log($"��ֹ� �浹, ��������");
                DestroyObstacle(collision.gameObject);
            }
        }
        //����
        if (collision.CompareTag("Finish"))
        {
            SurviveTime = initialSurviveTime;
        }
    }

    private void DestroyObstacle(GameObject go)
    {
        float animationTime = 3f;
        StartCoroutine(ObstacleDestroyEffect(go, animationTime));
    }
    private IEnumerator ObstacleDestroyEffect(GameObject go, float time) //���߿� �ð� �Ǹ� ������ �׸��鼭 ���ư��� ����.
    {
        float elapsed = 0f;
        while(go != null && elapsed <= time)
        {
            go.transform.rotation = Quaternion.Euler(0, 0, go.transform.rotation.eulerAngles.z + 35f);
            go.transform.position = new Vector3(go.transform.position.x + 0.5f, go.transform.position.y - 0.002f, go.transform.position.z);

            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(go);
    }
}
