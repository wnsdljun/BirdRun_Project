using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    UIManager _UIManager;

    public ObstacleSpawner obstacles { get; private set; }

    public bool isSpeedUp = false;

    /// <summary>
    /// 외부에서 인스턴스에 접근하기 위한 프로퍼티
    /// </summary>
    ///
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

    private Player _player;
    public Player player
    {
        get
        {
            if (_player == null)
            {
                Debug.Log("아직 플레이어 오브젝트 생성 전입니다.");
            }
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    //캐릭터 선택과 관련한 부분
    [SerializeField] private readonly List<GameObject> playableCharacters = new List<GameObject>();
    private GameObject SelectedCharater;
























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
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged; //씬 변경시 발생하는 이벤트 구독
        DontDestroyOnLoad(gameObject);

        _UIManager = FindAnyObjectByType<UIManager>();
        DontDestroyOnLoad(_UIManager.gameObject);

        obstacles = FindAnyObjectByType<ObstacleSpawner>();
    }

    #region 씬 변경과 관련한 부분. 씬에 따른 초기 설정 등.
    /// <summary>
    /// 게임매니저에 씬을 변경하라는 명령.
    /// </summary>
    /// <param name="sceneName">씬 이름</param>
    public void LoadScene(string sceneName)
    {
        //페이드 아웃 추가?

        //로드씬 불러오기
        SceneManager.LoadScene("02 LoadScene");
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string name)
    {
        float elapsed = 0f;
        float waitTime = 2f;
        if (SelectedCharater != null)
        {
            SceneManager.LoadScene($"{SelectedCharater.name}Scene", LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("선택된 새가 없습니다!");
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        asyncLoad.allowSceneActivation = false; //씬이 전부 로드되더라도 실행하지 않음.

        while (elapsed <= waitTime)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }
    }
    //씬 변경 이벤트.
    //씬이 변경되었다면 새로 호출된 씬 이름을 확인하여 동작 수행.
    private void SceneManager_activeSceneChanged(Scene prevScene, Scene newScene)
    {
        Debug.Log($"씬 변경됨: {prevScene.name} -> {newScene.name}");

        if (newScene.name.Equals("04 GameScene"))
        {
            //게임 씬 로드시 플레이어 스크립트가 붙은 오브젝트를 찾아
            //-> 플레이어니까, 선택한 캐릭터를 생성해 플레이어 오브젝트를 부모로 설정.
            _player = FindObjectOfType<Player>();
            Instantiate(SelectedCharater, _player.transform);
        }
    }


    #endregion


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
