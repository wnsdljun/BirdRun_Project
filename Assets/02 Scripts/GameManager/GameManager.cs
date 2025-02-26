using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    /// <summary>
    /// 외부에서 인스턴스에 접근하기 위한 프로퍼티
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
                    Debug.Log("싱글톤 인스턴스 없음.");
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
        //지형 생성
        //생성된 Obstacle이 20개 이하일 때만 추가로 생성
        if (obstacles.transform.childCount < 20)
        {
            if (!obstacles.isObstacle && Random.value > 0.5f) // 50% 확률로 장애물 생성
            {
                obstacles.SpawnObstacle();
            }
            else
            {
                obstacles.SpawnGround();
            }

            //지형이 생성 됐을 경우에만 아이템 생성되도록 함
            if (obstacles.isSpawner)
            {
                obstacles.itemSpawner = obstacles.lastObstacle.GetComponent<ItemSpawner>();
                obstacles.itemSpawner.SpawnItem();
            }
        }
    }
}
