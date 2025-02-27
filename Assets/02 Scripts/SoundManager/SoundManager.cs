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
                    Debug.Log("�̱��� �ν��Ͻ� ����");
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
            return; //���⼭ return �ؼ� ���� �ڵ带 �����Ű�� �ʾƾ� ������ ���°Ͱ���.
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
