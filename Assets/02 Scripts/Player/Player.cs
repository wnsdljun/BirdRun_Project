using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
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
    private void Slide()
    {
        isSliding = true;
        //애니메이터- 슬라이딩 true
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

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        //점프
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        //슬라이드
        if (isOnGround && Input.GetKeyDown(KeyCode.LeftShift)) Slide();
        if (isSliding && Input.GetKeyUp(KeyCode.LeftShift)) Slide_Getup();
    }

    private void FixedUpdate()
    {
        //생존시간 로직
        SurviveTime += Time.deltaTime;
        //이동속도 로직
        _rigidbody.velocity = new Vector2(moveSpeed, _rigidbody.velocity.y);
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
}
