using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

class GameManager
{
    static GameManager instance;

    int scoreCount = 0;

    UnityEvent onScoreChanged;

    public static GameManager Instance()
    {
        instance = instance ?? new GameManager();

        return instance;
    }

    public void SubscribeOnScoreChanged(UnityAction action)
    {
        if (onScoreChanged == null)
            onScoreChanged = new UnityEvent();

        onScoreChanged.AddListener(action);
    }

    public void ResetOnScoreChanged()
    {
        if (onScoreChanged == null)
            onScoreChanged = new UnityEvent();

        onScoreChanged.RemoveAllListeners();
    }

    public void AddScore(int scores)
    {
        scoreCount += scores;

        onScoreChanged?.Invoke();
    }

    public int GetScoreCount()
    {
        return scoreCount;
    }

    public void Reset()
    {
        scoreCount = 0;
        ResetOnScoreChanged();
    }
}
