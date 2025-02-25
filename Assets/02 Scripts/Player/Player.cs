using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Animator _animator;
    [SerializeField] private float moveSpeed = 3f;

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

    //�����̵� ���¸� �����ϸ� �ִϸ����͵� �� �ٲ���.
    public bool IsSliding
    {
        get => isSliding;
        set
        {
            if (isSliding != value)
            {
                isSliding = value;
                _animator.SetBool("IsSlide", isSliding);
            }

        }
    }
    private void Slide()
    {
        isSliding = true;
    }
    private void Slide_Getup()
    {
        isSliding = false;
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
        SurviveTime -= amount;
        Debug.Log($"�����ð� \"{amount}\" ȸ��. total: {SurviveTime}");
    }
    private float speedBoost = 0;
    private float speedBoostDuration = 0;
    public void SpeedUp(float speed, float duration)
    {
        speedBoost = speed;
        speedBoostDuration = duration;
        Debug.Log($"���ǵ�� for [{duration}]");
    }
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        //�����ð� ����
        SurviveTime += Time.deltaTime;
        //�̵��ӵ� ����
        _rigidbody.velocity = new Vector2(moveSpeed + speedBoost, _rigidbody.velocity.y);
        if (speedBoostDuration >= 0) speedBoostDuration -= Time.fixedDeltaTime;
        else speedBoost = 0f;
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
    }
}
