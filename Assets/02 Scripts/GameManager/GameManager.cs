using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    UIManager _UIManager;

    public ObstacleSpawner obstacles { get; private set; }
    [SerializeField] bool isCutScenePlayDone = true;
    public bool isSpeedUp = false;
    private float playTime = 0;
    public float PlayTime
    {
        get => playTime;
    }

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
    public GameObject SelectedCharater;
























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
        player = FindAnyObjectByType<Player>();
        //DontDestroyOnLoad(_UIManager.gameObject);

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
        //StartCoroutine(AwaitLoadScene(sceneName));
        //�ε���� ������ �ε� �� �� �״��� ���� ȣ������ߵ�. ��...?

        string str = sceneName.ToString();
        SceneManager.LoadSceneAsync("02 LoadScene");
        StartCoroutine(LoadSceneCoroutine(str));
    }
    private IEnumerator LoadSceneCoroutine(string name)
    {
        float elapsed = 0f;
        float waitTime = 1.5f;
        
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
        
        while (elapsed <= waitTime && !isCutScenePlayDone)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
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





}
