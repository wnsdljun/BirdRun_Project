using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    //[SerializeField] private Text waveText;
    //[SerializeField] private Slider hpSlider;
    //[SerializeField] private Button pauseButton;
    [SerializeField] private Image hpBar;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text fruitText;
    [SerializeField] private Text speedUpText;

    private void Start()
    {
        //UpdateHPSlider(1);
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        //pauseButton.onClick.AddListener(OnClickPauseButton);
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

    IEnumerator SpeedUpTextCoroutine()
    {
        speedUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        speedUpText.gameObject.SetActive(false);
    }

    //public void UpdateHPSlider(float percentage)
    //{
    //    hpSlider.value = percentage;
    //}

    //public void UpdateWaveText(int wave)
    //{
    //    waveText.text = wave.ToString();
    //}

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}