using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance().ShopButtonClick();
    }
}
