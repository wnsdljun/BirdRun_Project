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
    [SerializeField] private float collisionPenalty = 5f; //충돌 시 감소시킬 시간

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
    private Vector2 colliderOffsetAtStand;
    private Vector2 colliderSizeAtStand;
    private Vector2 colliderOffsetAtSlide;
    private Vector2 colliderSizeAtSlide;
    //슬라이딩 상태를 변경하면 애니메이터도 값 바꿔줌.
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
        if (isSliding) //슬라이드중이라면
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
        SurviveTime -= SurviveTime * amount;
        Debug.Log($"생존시간 \"{amount}\" 회복. total: {SurviveTime}");
    }
    private float speedBoost = 0f;
    private float speedBoostDuration = 0f;
    public void SpeedUp(float speed, float duration)
    {
        speedBoost = speed;
        speedBoostDuration = duration;
        Debug.Log($"스피드업 for [{duration}]");
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

        #region 슬라이딩 할때 콜라이더 크기 바꿔주는 부분. 크기를 불러와 저장한다.
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
        //이동속도 로직
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
        //장애물 충돌시 로직
        if (collision.CompareTag("Obstacle"))
        {
            if (speedBoostDuration <= 0) //가속중이 아니면
            {
                Debug.Log($"장애물 충돌, 생존시간 증가 현재 {SurviveTime}");
                SurviveTime += collisionPenalty;
            }
            else //가속중일때
            {
                Debug.Log($"장애물 충돌, 무적상태");
                DestroyObstacle(collision.gameObject);
            }
        }
        //낙사
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
    private IEnumerator ObstacleDestroyEffect(GameObject go, float time) //나중에 시간 되면 포물선 그리면서 날아가게 수정.
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
