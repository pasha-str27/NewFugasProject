using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconBehaviour : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] Vector2 inputSize;
    [SerializeField] Vector2 inputOffet;
    [SerializeField] float inputDelay = 0.2f;
    [SerializeField] float eps = .05f;
    [SerializeField] CustomScrollRect scrollRect; 

    Rect inputZone;

    private void Start()
    {
        inputZone = new Rect(inputOffet, inputSize);

        if (!PlayerPrefs.GetString("AccessibleBalls").Contains(id.ToString()))
            gameObject.SetActive(false);

        StartCoroutine(CheckIconPosition());
    }

    IEnumerator CheckIconPosition()
    {
        while(true)
        {
            if (id != PlayerPrefs.GetInt("ChosenBall") && inputZone.Contains(transform.position))
            {
                float integerPos = scrollRect.GetHorNormPos() / scrollRect.GetInputDelta();
                float newPos = Mathf.RoundToInt(integerPos) * scrollRect.GetInputDelta();

                if(Mathf.Abs(newPos - integerPos) > eps)
                    scrollRect.SetHorNormPos(newPos);

                GameManager.Instance().SetNewBall(id);
            }    

            yield return new WaitForSeconds(inputDelay);
        }
    }
}
