using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [SerializeField] private Button keepButton;
    [SerializeField] private Button endButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        keepButton.onClick.AddListener(OnClickKeepPlayButton);
        endButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickKeepPlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.End;
    }
}
