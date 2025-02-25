using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    /// <summary>
    /// �ܺο��� �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                {
                    Debug.Log("�̱��� �ν��Ͻ� ����.");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
