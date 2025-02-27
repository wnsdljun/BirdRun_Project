using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text fruitText;
    [SerializeField] private Text speedUpText;


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    public void OnClickPauseButton()
    {
        Time.timeScale = 0f;
    }

    public void UpdateHPBar(float playerHP)
    {
        hpBar.fillAmount = 1 - playerHP;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString("N0");
    }

    public void UpdateFruit(int score)
    {
        fruitText.text = score.ToString("N0");
    }

    public void ShowSpeedUpText()
    {
        StartCoroutine(SpeedUpTextCoroutine());
    }

    public void ResetUI()
    {
        scoreText.text = "0";
        fruitText.text = "0";
    }

    IEnumerator SpeedUpTextCoroutine()
    {
        speedUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(GetComponentInChildren<Animator>().runtimeAnimatorController.animationClips[0].length);
        speedUpText.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}