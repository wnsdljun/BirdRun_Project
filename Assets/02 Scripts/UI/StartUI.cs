using UnityEngine;
using UnityEngine.UI;

public class StartUI : BaseUI
{
    [SerializeField] private Button startButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        startButton.onClick.AddListener(OnClickStartButton);
    }

    public void OnClickStartButton()
    {
        GameManager.Instance.LoadScene("03 SelectScene");
    }

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }
}