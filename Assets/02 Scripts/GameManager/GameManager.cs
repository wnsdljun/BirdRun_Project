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
    /// �ܺο��� �ν��Ͻ��� �����ϱ� ���� ������Ƽ
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
                    Debug.Log("�̱��� �ν��Ͻ� ����.");
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
                Debug.Log("���� �÷��̾� ������Ʈ ���� ���Դϴ�.");
            }
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    //ĳ���� ���ð� ������ �κ�
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
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged; //�� ����� �߻��ϴ� �̺�Ʈ ����
        DontDestroyOnLoad(gameObject);

        _UIManager = FindAnyObjectByType<UIManager>();
        DontDestroyOnLoad(_UIManager.gameObject);

        obstacles = FindAnyObjectByType<ObstacleSpawner>();
    }

    #region �� ����� ������ �κ�. ���� ���� �ʱ� ���� ��.
    /// <summary>
    /// ���ӸŴ����� ���� �����϶�� ���.
    /// </summary>
    /// <param name="sceneName">�� �̸�</param>
    public void LoadScene(string sceneName)
    {
        //���̵� �ƿ� �߰�?

        //�ε�� �ҷ�����
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
            Debug.Log("���õ� ���� �����ϴ�!");
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        asyncLoad.allowSceneActivation = false; //���� ���� �ε�Ǵ��� �������� ����.

        while (elapsed <= waitTime)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }
    }
    //�� ���� �̺�Ʈ.
    //���� ����Ǿ��ٸ� ���� ȣ��� �� �̸��� Ȯ���Ͽ� ���� ����.
    private void SceneManager_activeSceneChanged(Scene prevScene, Scene newScene)
    {
        Debug.Log($"�� �����: {prevScene.name} -> {newScene.name}");

        if (newScene.name.Equals("04 GameScene"))
        {
            //���� �� �ε�� �÷��̾� ��ũ��Ʈ�� ���� ������Ʈ�� ã��
            //-> �÷��̾�ϱ�, ������ ĳ���͸� ������ �÷��̾� ������Ʈ�� �θ�� ����.
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
