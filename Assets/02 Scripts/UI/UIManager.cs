using UnityEngine;

public enum UIState
{
    Start,
    Game,
    Pause,
    End,
    Empty
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(UIManager)) as UIManager;

                if (instance == null)
                {
                    Debug.Log("ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫ æ¯¿Ω");
                }
            }
            return instance;
        }
    }

    StartUI startUI;
    GameUI gameUI;
    PauseUI pauseUI;
    EndUI endUI;
    private UIState currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        DontDestroyOnLoad(gameObject);

        startUI = GetComponentInChildren<StartUI>(true);
        startUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        endUI = GetComponentInChildren<EndUI>(true);
        endUI.Init(this);

        ChangeState(UIState.Start);
    }
    public void SetStartGame()
    {
        ChangeState(UIState.Start);
    }
    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetEndGame(int seedScore, int fruitScore)
    {
        endUI.SetUI(seedScore, fruitScore);
        ChangeState(UIState.End);
    }

    public void SetPauseGame()
    {
        ChangeState(UIState.Pause);
    }
    public void SetUIOff()
    {
        ChangeState(UIState.Empty);
    }
    public void ChangePlayerHp(float playerHP)
    {
        gameUI.UpdateHPBar(playerHP);
    }

    public void ChangeScore(int score)
    {
        gameUI.UpdateScore(score);
    }

    public void ChangeFruit(int score)
    {
        gameUI.UpdateFruit(score);
    }

    public void DisplaySpeedUpText()
    {
        gameUI.ShowSpeedUpText();
    }

    public void ResetUI()
    {
        gameUI.ResetUI();
        endUI.ResetUI();
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        endUI.SetActive(currentState);
    }
}