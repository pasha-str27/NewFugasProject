using UnityEngine;

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
        GameManager.Instance().SubscribeOnCoinsAmountChanged(delegate {ResetInUseButton(); });

        ResetInUseButton();
    }

    public void ResetInUseButton()
    {
        if (PlayerPrefs.GetInt("ChosenBall") != id)
        {
            print("Wrong ball was found " + id);
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
            print("Ball was found");
            buyButton.SetActive(false);
            useButton.SetActive(false);
            inUseButton.SetActive(true);
        }
    }

    public void OnBuyButtonClick()
    {
        if (PlayerPrefs.GetInt("Coins") >= int.Parse(cost.text))
        {
            GameManager.Instance().ChangeCoinsAmount(-1 * int.Parse(cost.text));
            PlayerPrefs.SetString("AccessibleBalls", PlayerPrefs.GetString("AccessibleBalls") + id);
        }
        else
            Debug.LogError("Not enought money");

        ResetInUseButton();
    }

    public void SetBallInUse()
    {
        GameManager.Instance().SetNewBall(id);
        ResetInUseButton();
    }
}
