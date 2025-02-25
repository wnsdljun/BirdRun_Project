using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Button pauseButton;

    private void Start()
    {
        UpdateHPSlider(1);
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        pauseButton.onClick.AddListener(OnClickPauseButton);
    }

    public void OnClickPauseButton()
    {
        Time.timeScale = 0f;
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}