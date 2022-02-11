using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPanelBehaviour : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] TMPro.TextMeshProUGUI cost;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject useButton;
    [SerializeField] GameObject inUseButton;

    void Start()
    {
        GameManager.Instance().SubscribeOnBallChanged(delegate {ResetInUseButton(); });
        ResetInUseButton();
    }

    public void ResetInUseButton()
    {
        if (PlayerPrefs.GetInt("ChosenBall") != id)
        {
            buyButton.SetActive(true);
            useButton.SetActive(false);
            inUseButton.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("AccessibleBalls").Contains(id.ToString()))
        {
            buyButton.SetActive(false);
            useButton.SetActive(true);
            inUseButton.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ChosenBall") == id)
        {
            buyButton.SetActive(false);
            useButton.SetActive(false);
            inUseButton.SetActive(true);
        }
    }

    public void OnBuyButtonClick()
    {
        GameManager.Instance().ChangeCoinsAmount(-1*int.Parse(cost.text));
        PlayerPrefs.SetString("AccessibleBalls", PlayerPrefs.GetString("AccessibleBalls") + id);
        print(PlayerPrefs.GetString("AccessibleBalls"));
    }

    public void SetBallInUse()
    {
        GameManager.Instance().SetNewBall(id);
    }
}
