using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsPositionUpdater : MonoBehaviour
{
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject downWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject topWall;

    void Start()
    {
        var screenLimit = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        rightWall.transform.position = new Vector2(screenLimit.x, 0);
        leftWall.transform.position = new Vector2(-screenLimit.x, 0);

        print(screenLimit);

        Destroy(this);   
    }
}
