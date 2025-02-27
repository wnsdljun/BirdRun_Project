using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (instance == null)
                {
                    Debug.Log("싱글톤 인스턴스 없음");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
            return; //여기서 return 해서 추후 코드를 실행시키지 않아야 오류가 없는것같음.
        }
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        DontDestroyOnLoad(gameObject);

        mainAudioSource = GetComponent<AudioSource>();
    }

    [SerializeField] AudioClip mainSceneAClip;
    [SerializeField] AudioClip selectSceneAClip;
    [SerializeField] AudioClip gameSceneAClip;

    private AudioSource mainAudioSource;

    void SceneManager_activeSceneChanged(Scene prevScene, Scene newScene)
    {
        if (instance != null)
        {
            if (newScene.name.Equals("01 StartScene"))
            {
                mainAudioSource.clip = mainSceneAClip;
                mainAudioSource.Play();
            }
            else if (newScene.name.Equals("02 LoadScene"))
            {
                if (GameManager.Instance.SelectedCharater == null)
                    mainAudioSource.Stop();
            }
            else if (newScene.name.Equals("03 SelectScene"))
            {
                mainAudioSource.clip = selectSceneAClip;
                mainAudioSource.Play();
            }
            else if (newScene.name.Equals("04 GameScene"))
            {
                mainAudioSource.clip = gameSceneAClip;
                mainAudioSource.Play();
            }
        }
    }
}
