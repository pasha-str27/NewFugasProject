using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI scoreHUD;
    [SerializeField] TMPro.TextMeshProUGUI scoreEndGame;
    [SerializeField] TMPro.TextMeshProUGUI scoreRecord;
    [SerializeField] GameObject lifeContainer;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject infoPanel;

    void Start()
    {
        GameManager.Instance().Reset();
        //PlayerPrefs.DeleteAll();

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            GameManager.Instance().SubscribeOnCoinsAmountChanged(delegate { UpdateCoinsAmountLabel(); });
            GameManager.Instance().SubscribeOnShopButtonClick(delegate { UpdateCoinsAmountLabel(); });
        }
        else
        {
            GameManager.Instance().SubscribeOnScoreChanged(delegate { UpdateGameScoreLabel(); });
            GameManager.Instance().SubscribeOnGameOver(delegate { Time.timeScale = 0; ShowEndGamePanel(); });
            GameManager.Instance().SubscribeOnLifesChanged(delegate { UpdateLifesAmount(); });
        }

        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 950);
            GameManager.Instance().ChangeCoinsAmount(0);
        }

        if (!PlayerPrefs.HasKey("ChosenBall"))
        {
            PlayerPrefs.SetInt("ChosenBall", 0);
            PlayerPrefs.SetString("AccessibleBalls", "0");
            GameManager.Instance().SetNewBall(0);
        }

        if (!PlayerPrefs.HasKey("Record"))
        {
            PlayerPrefs.SetInt("Record", 0);
        }
    }

    public void UpdateLifesAmount()
    {
        lifeContainer.transform.GetChild(GameManager.Instance().GetLifesCount())?.gameObject.SetActive(false);

        if (GameManager.Instance().GetLifesCount() == 0)
            GameManager.Instance().GameOver();
    }

    void ShowEndGamePanel()
    {
        endGamePanel?.SetActive(true);
        infoPanel?.SetActive(false);

        int score = GameManager.Instance().GetScoreCount();
        int record = PlayerPrefs.GetInt("Record");

        scoreEndGame.text = score.ToString();
        scoreRecord.text = record <= score ? score.ToString() : record.ToString();

        GameManager.Instance().TogglePause(true);
    }

    void UpdateGameScoreLabel()
    {
        scoreHUD.text = GameManager.Instance().GetScoreCount().ToString();
    }

    void UpdateCoinsAmountLabel()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            var newScore = PlayerPrefs.GetInt("Coins").ToString();
            if (newScore != "")
                scoreHUD.text = newScore;
        }
        else
            scoreHUD.text = "0";
    }

    public void Pause() => GameManager.Instance().TogglePause(true);
    public void Unpause() => GameManager.Instance().TogglePause(false);

    public void LoadMainMenuScene() => SceneManager.LoadScene("Menu");
    public void LoadGameScene() => SceneManager.LoadScene("Game");
    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void Exit() => Application.Quit();
}
