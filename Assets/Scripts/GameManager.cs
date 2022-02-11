using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

class GameManager
{
    static GameManager instance;

    int scoreCount = 0;
    int lifeCount = 3;

    UnityEvent onCoinsAmountChanged;
    UnityEvent onLifesChanged;
    UnityEvent onBallChanged;
    UnityEvent onScoreChanged;
    UnityEvent onGameOver;

    public static GameManager Instance()
    {
        instance = instance ?? new GameManager();

        return instance;
    }

    public void GameOver()
    {
        Debug.LogError("Game over");

        onGameOver?.Invoke();
    }

    public void SubscribeOnScoreChanged(UnityAction action)
    {
        if (onScoreChanged == null)
            onScoreChanged = new UnityEvent();

        onScoreChanged.AddListener(action);
    }
    public void SubscribeOnLifesChanged(UnityAction action)
    {
        if (onLifesChanged == null)
            onLifesChanged = new UnityEvent();

        onLifesChanged.AddListener(action);
    }

    public void SubscribeOnGameOver(UnityAction action)
    {
        if (onGameOver == null)
            onGameOver = new UnityEvent();

        onGameOver.AddListener(action);
    }

    public void SubscribeOnCoinsAmountChanged(UnityAction action)
    {
        if (onCoinsAmountChanged == null)
            onCoinsAmountChanged = new UnityEvent();

        onCoinsAmountChanged.AddListener(action);
    }

    public void SubscribeOnBallChanged(UnityAction action)
    {
        if (onBallChanged == null)
            onBallChanged = new UnityEvent();

        onBallChanged.AddListener(action);
    }

    public void ResetOnScoreChanged()
    {
        if (onScoreChanged == null)
            onScoreChanged = new UnityEvent();

        onScoreChanged.RemoveAllListeners();
    }

    public void ResetOnGameOver()
    {
        if (onGameOver == null)
            onGameOver = new UnityEvent();

        onGameOver.RemoveAllListeners();
    }

    public void ChangeCoinsAmount(int value)
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
            Debug.LogError("New coins amount: " + PlayerPrefs.GetInt("Coins"));
        }

        onCoinsAmountChanged?.Invoke();
    }

    public void SetNewBall(int id)
    {
        if (PlayerPrefs.HasKey("ChosenBall"))
        {
            PlayerPrefs.SetInt("ChosenBall", id);
            Debug.LogError("New ball id: " + id);
        }
        else
            Debug.LogError("There is no ball setted");

        onBallChanged?.Invoke();
    }

    public void AddScore(int scores)
    {
        scoreCount += scores;

        onScoreChanged?.Invoke();
    }

    public void MinusLife()
    {
        --lifeCount;
        onLifesChanged?.Invoke();
    }

    public int GetLifesCount() => lifeCount; 

    public int GetScoreCount() => scoreCount;

    public void Reset()
    {
        scoreCount = 0;
        lifeCount = 3;
        ResetOnScoreChanged();
        ResetOnGameOver();
    }
}
