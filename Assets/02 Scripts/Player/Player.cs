using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Animator _animator;
    [SerializeField] private float moveSpeed = 3f;

    #region 점프 관련 로직
    //Update에서 입력을 받고 
    [SerializeField] private float jumpPower = 20f;

    private bool isOnGround = false;
    private bool isDoubleJumped = false;
    private void Jump()
    {
        Debug.Log("점프 호출됨.");
        if (isDoubleJumped) return; //2단점프를 했다면 돌아가기.
        if (isOnGround) isOnGround = false;
        else if (!isOnGround && !isDoubleJumped) isDoubleJumped = true;//땅에 붙어있지 않다면 수직 속도 0
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    #endregion
    #region 슬라이드 관련 로직
    private bool isSliding = false;

    //슬라이딩 상태를 변경하면 애니메이터도 값 바꿔줌.
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
    #region 하트 관련 로직은 여기에
    [SerializeField] private float initialSurviveTime;//생존 시간- 에디터에서 수정
    private float surviveTime = 0f; //이 값이 증가하며 초기 생존 시간 값보다 크거나 같게되면 게임오버
    public float SurviveTime
    {
        get => surviveTime;
        set
        {
            //하트 물약을 많이 먹어서 생존시간이 음수로 내려가는 경우?
            surviveTime = value;
            if (surviveTime >= initialSurviveTime) TimeOver();
        }
    }

    private void TimeOver()
    {
        Debug.Log("시간 다 됐다");
        Time.timeScale = 0;
    }
    #endregion
    #region 아이템 사용 로직은 여기에
    public void Heal(float amount)
    {
        SurviveTime -= amount;
        Debug.Log($"생존시간 \"{amount}\" 회복. total: {SurviveTime}");
    }
    private float speedBoost = 0;
    private float speedBoostDuration = 0;
    public void SpeedUp(float speed, float duration)
    {
        speedBoost = speed;
        speedBoostDuration = duration;
        Debug.Log($"스피드업 for [{duration}]");
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
        //점프
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        //슬라이드
        if (isOnGround && Input.GetKeyDown(KeyCode.LeftShift)) 
            IsSliding = true;
        if (isSliding && Input.GetKeyUp(KeyCode.LeftShift)) 
            IsSliding = false;
    }

    private void FixedUpdate()
    {
        //생존시간 로직
        SurviveTime += Time.deltaTime;
        //이동속도 로직
        _rigidbody.velocity = new Vector2(moveSpeed + speedBoost, _rigidbody.velocity.y);
        if (speedBoostDuration >= 0) speedBoostDuration -= Time.fixedDeltaTime;
        else speedBoost = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //지면과 충돌시 로직
        if (collision.gameObject.CompareTag("Ground"))
        {
            isDoubleJumped = false;
            isOnGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //아이템이 플레이어와 가까워 충돌했다면
        if (collision.CompareTag("Item"))
        {
            IItemEffect item = collision.gameObject.GetComponent<IItemEffect>();
            item.ApplyEffect(this);
            Destroy(collision.gameObject);
        }
    }
}
