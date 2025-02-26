using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    #region �� ����� ������ �κ�. ���� ���� �ʱ� ���� ��.
    //�� ���� �̺�Ʈ
    private void SceneManager_activeSceneChanged(Scene prevScene, Scene newScene)
    {
        Debug.Log($"�� �����: {prevScene.name} -> {newScene.name}");

        switch (newScene.name)
        {
            case "01 StartScene":
                WhenSceneLoaded_StartScene();
                break;
            case "02 LoadScene":
                WhenSceneLoaded_LoadScene();
                break;
            case "03 SelectScene":
                WhenSceneLoaded_SelectScene();
                break;
            case "04 GameScene":
                WhenSceneLoaded_GameScene();
                break;
            default:

                break;
        }
    }

    private void WhenSceneLoaded_StartScene()
    {

    }
    private void WhenSceneLoaded_LoadScene()
    {
        if (SelectedCharater != null)
        {
            if (SelectedCharater.name.Equals("Superba")) //�ض��� ������Ʈ�� �̸��� �̰ɷ�.
            {
                //�ε��߿� ������ �ƾ� ����
                SceneManager.LoadScene("SuperbaScene", LoadSceneMode.Additive);
            }
            else if (SelectedCharater.name.Equals("Montanus")) //���� ������Ʈ�� �̸��� �̰ɷ�.
            {
                //�ε��߿� ������ �ƾ� ����
                //SceneManager.LoadScene("MontanusScene", LoadSceneMode.Additive);

            }


        }
        else
        {
            Debug.Log("���õ� ���� �����ϴ�!");
        }
    }
    private void WhenSceneLoaded_SelectScene()
    {

    }
    private void WhenSceneLoaded_GameScene()
    {
        //���� �� �ε�� �÷��̾� ��ũ��Ʈ�� ���� ������Ʈ�� ã��
        //-> �÷��̾�ϱ�, ������ ĳ���͸� ������ �÷��̾� ������Ʈ�� �θ�� ����.
        _player = FindObjectOfType<Player>();
        Instantiate(SelectedCharater, _player.transform);
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

    }


    private IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("04 GameScene");
        asyncLoad.allowSceneActivation = false; //���� ���� �ε�Ǵ��� �������� ����.

        yield return null;
    }
}
