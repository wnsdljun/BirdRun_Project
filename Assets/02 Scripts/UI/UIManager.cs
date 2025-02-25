using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Start,
    Game,
    Pause,
    End,
}

public class UIManager : MonoBehaviour
{
    StartUI startUI;
    GameUI gameUI;
    PauseUI pauseUI;
    EndUI endUI;
    private UIState currentState;

    private void Awake()
    {
        startUI = GetComponentInChildren<StartUI>(true);
        startUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        //gameUI.Init(this);
        pauseUI = GetComponentInChildren<PauseUI>(true);
        pauseUI.Init(this);
        endUI = GetComponentInChildren<EndUI>(true);
        endUI.Init(this);

        ChangeState(UIState.Start);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetEndGame()
    {
        ChangeState(UIState.End);
    }

    //public void ChangeWave(int waveIndex)
    //{
    //    gameUI.UpdateWaveText(waveIndex);
    //}

    //public void ChangePlayerHP(float currentHP, float maxHP)
    //{
    //    gameUI.UpdateHPSlider(currentHP / maxHP);
    //}

    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        pauseUI.SetActive(currentState);
        endUI.SetActive(currentState);
    }
}