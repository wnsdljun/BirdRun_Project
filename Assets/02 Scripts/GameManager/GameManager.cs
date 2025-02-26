using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    /// <summary>
    /// �ܺο��� �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    /// </summary>
    ///

    public ObstacleSpawner obstacles { get; private set; }

    public bool isSpeedUp = false;

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

        obstacles = FindAnyObjectByType<ObstacleSpawner>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene();
        //SceneManager.LoadSceneAsync();
    }

    // Update is called once per frame
    void Update()
    {
        //���� ����
        //������ Obstacle�� 20�� ������ ���� �߰��� ����
        if (obstacles.transform.childCount < 20)
        {
            if (!obstacles.isObstacle && Random.value > 0.5f) // 50% Ȯ���� ��ֹ� ����
            {
                obstacles.SpawnObstacle();
            }
            else
            {
                obstacles.SpawnGround();
            }

            //������ ���� ���� ��쿡�� ������ �����ǵ��� ��
            if (obstacles.isSpawner)
            {
                obstacles.itemSpawner = obstacles.lastObstacle.GetComponent<ItemSpawner>();
                obstacles.itemSpawner.SpawnItem();
            }
        }
    }
}
