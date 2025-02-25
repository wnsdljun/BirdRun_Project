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
        //GameManager.instance.StartGame();
    }

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }
}