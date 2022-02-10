using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] TMPro.TextMeshProUGUI scoreHUD;
    [SerializeField] TMPro.TextMeshProUGUI scoreEndGame;
    [SerializeField] TMPro.TextMeshProUGUI scoreRecord;
    [SerializeField] GameObject lifeContainer;
    [SerializeField] GameObject endGamePanel;

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
        {
            GameManager.Instance().SubscribeOnScoreChanged(delegate { UpdateGameScoreLabel(); });
            GameManager.Instance().SubscribeOnGameOver(delegate { ShowEndGamePanel(); });
            GameManager.Instance().SubscribeOnLifesChanged(delegate { UpdateLifesAmount(); });
        }

        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 5000);
            GameManager.Instance().ChangeCoinsAmount(0);
        }

        if (!PlayerPrefs.HasKey("ChosenBall"))
        {
            PlayerPrefs.SetInt("ChosenBall", 0);
            PlayerPrefs.SetString("AccessibleBalls", "0123");
            GameManager.Instance().SetNewBall(0);
        }

        if (!PlayerPrefs.HasKey("Record"))
        {
            PlayerPrefs.SetInt("Record", 200);
        }
    }

    public void UpdateLifesAmount()
    {
        lifeContainer.transform.GetChild(GameManager.Instance().GetLifesCount() + 1)?.gameObject.SetActive(false);
    }

    public void UpdateItemPanelLabels()
    {
        GameManager.Instance().SetNewBall(PlayerPrefs.GetInt("ChosenBall"));
    }

    void ShowEndGamePanel()
    {
        endGamePanel.SetActive(true);
        scoreRecord.text = PlayerPrefs.GetInt("Record").ToString();
    }

    void UpdateGameScoreLabel()
    {
        Debug.LogError("Score updated");
        scoreHUD.text = GameManager.Instance().GetScoreCount().ToString();
        scoreEndGame.text = GameManager.Instance().GetScoreCount().ToString();
    }

    void UpdateCoinsAmountLabel()
    {
        if (PlayerPrefs.HasKey("Coins"))
            scoreHUD.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void LoadMainMenuScene() => SceneManager.LoadScene("Menu");
    public void LoadGameScene() => SceneManager.LoadScene("Game");
    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void Exit() => Application.Quit();
}
