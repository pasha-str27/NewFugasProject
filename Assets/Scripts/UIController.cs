using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public TMPro.TextMeshProUGUI score;
    public GameObject lifeContainer;

    private void Awake() {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
            GameManager.Instance().SubscribeOnCoinsAmountChanged(delegate { UpdateCoinsAmountLabel(); });
        else
            GameManager.Instance().SubscribeOnScoreChanged(delegate { UpdateGameScoreLabel(); });

        //if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 5000);
            GameManager.Instance().ChangeCoinsAmount(0);
        }

        //if (!PlayerPrefs.HasKey("ChosenBall"))
        {
            PlayerPrefs.SetInt("ChosenBall", 0);
            PlayerPrefs.SetString("AccessibleBalls", "01234");
            GameManager.Instance().SetNewBall(0);
        }
    }

    void UpdateGameScoreLabel()
    {
        Debug.LogError("Score updated");
        score.text = GameManager.Instance().GetScoreCount().ToString();
    }

    void UpdateCoinsAmountLabel()
    {
        if (PlayerPrefs.HasKey("Coins"))
            score.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Debug.LogError("Exit!");
        Application.Quit();
    }
}
