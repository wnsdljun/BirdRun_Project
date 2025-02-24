using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    #region Jump
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


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isDoubleJumped = false;
            isOnGround = true;
        }
    }
}
