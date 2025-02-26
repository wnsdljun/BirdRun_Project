using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    /// <summary>
    /// 외부에서 인스턴스에 접근하기 위한 프로퍼티
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
    }

    #region 씬 변경과 관련한 부분. 씬에 따른 초기 설정 등.
    //씬 변경 이벤트
    private void SceneManager_activeSceneChanged(Scene prevScene, Scene newScene)
    {
        Debug.Log($"씬 변경됨: {prevScene.name} -> {newScene.name}");

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
            if (SelectedCharater.name.Equals("Superba")) //극락조 오브젝트의 이름은 이걸로.
            {
                //로드중에 보여줄 컷씬 설정
                SceneManager.LoadScene("SuperbaScene", LoadSceneMode.Additive);
            }
            else if (SelectedCharater.name.Equals("Montanus")) //참새 오브젝트의 이름은 이걸로.
            {
                //로드중에 보여줄 컷씬 설정
                //SceneManager.LoadScene("MontanusScene", LoadSceneMode.Additive);

            }


        }
        else
        {
            Debug.Log("선택된 새가 없습니다!");
        }
    }
    private void WhenSceneLoaded_SelectScene()
    {

    }
    private void WhenSceneLoaded_GameScene()
    {
        //게임 씬 로드시 플레이어 스크립트가 붙은 오브젝트를 찾아
        //-> 플레이어니까, 선택한 캐릭터를 생성해 플레이어 오브젝트를 부모로 설정.
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
        asyncLoad.allowSceneActivation = false; //씬이 전부 로드되더라도 실행하지 않음.

        yield return null;
    }
}
