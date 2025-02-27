using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public UIManager _UIManager;

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
                //�������� �ǽ�?
                //Debug.Log("���� �÷��̾� ������Ʈ ���� ���Դϴ�.");
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
        DontDestroyOnLoad(_UIManager.gameObject);

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
        yield return null;
        float elapsed = 0f;
        float waitTime = 1.5f;
        
        if (SelectedCharater != null)
        {
            yield return SceneManager.LoadSceneAsync($"{SelectedCharater.name}Scene", LoadSceneMode.Additive);
            
            StartCoroutine(PlayCutScene());
        }
        else
        {
            Debug.Log("���õ� ���� �����ϴ�!");
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        yield return null;
        asyncLoad.allowSceneActivation = false; //���� ���� �ε�Ǵ��� �������� ����.
        
        while (!isCutScenePlayDone || elapsed <= waitTime)
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
            _UIManager.SetPlayGame();
            //���� �� �ε�� �÷��̾� ��ũ��Ʈ�� ���� ������Ʈ�� ã��
            //-> �÷��̾�ϱ�, ������ ĳ���͸� ������ �÷��̾� ������Ʈ�� �θ�� ����.
            _player = FindObjectOfType<Player>();
            Instantiate(SelectedCharater, _player.transform);
            obstacles = FindAnyObjectByType<ObstacleSpawner>();
        }
        if (newScene.name.Equals("02 LoadScene") || newScene.name.Equals("03 SelectScene"))
        {
            _UIManager.SetUIOff();
        }
        if (newScene.name.Equals("01 StartScene"))
        {
            _UIManager.SetStartGame();
        }
    }
    private IEnumerator PlayCutScene()
    {
        isCutScenePlayDone = false;
        yield return null;
        GameObject cutsceneobj = GameObject.Find($"{SelectedCharater.name}Cut");
        float playTime = cutsceneobj.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;

        yield return new WaitForSeconds(playTime);
        isCutScenePlayDone = true;

    }

    #endregion





}
