using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button endButton;
    [SerializeField] private Text seedScore;
    [SerializeField] private Text fruitScore;
    [SerializeField] private Image montanusEnd;
    [SerializeField] private Image superbaEnd;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        restartButton.onClick.AddListener(OnClickRestartButton);
        endButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetGameManager();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExitButton()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetGameManager();
        GameManager.Instance.SelectedCharater = null;
        GameManager.Instance.LoadScene("01 StartScene");
    }

    public void SetUI(int score, int fruit)
    {
        if (GameManager.Instance.SelectedCharater.name == "Montanus")
        {
            montanusEnd.gameObject.SetActive(true);
            superbaEnd.gameObject.SetActive(false);
        }
        else if (GameManager.Instance.SelectedCharater.name == "Superba")
        {
            montanusEnd.gameObject.SetActive(false);
            superbaEnd.gameObject.SetActive(true);
        }

        seedScore.text = score.ToString("N0");
        fruitScore.text = fruit.ToString("N0");
    }
    public void ResetUI()
    {
        seedScore.text = "0";
        fruitScore.text = "0";
    }

    protected override UIState GetUIState()
    {
        return UIState.End;
    }
}