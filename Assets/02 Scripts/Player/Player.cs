using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
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
    private void Slide()
    {
        isSliding = true;
        //�ִϸ�����- �����̵� true
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

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        //����
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        //�����̵�
        if (isOnGround && Input.GetKeyDown(KeyCode.LeftShift)) Slide();
        if (isSliding && Input.GetKeyUp(KeyCode.LeftShift)) Slide_Getup();
    }

    private void FixedUpdate()
    {
        //�����ð� ����
        SurviveTime += Time.deltaTime;
        //�̵��ӵ� ����

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
}
