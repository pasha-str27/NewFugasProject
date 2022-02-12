using System.Collections;
using UnityEngine;

public class ItemIconBehaviour : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] BoxCollider2D inputZone;
    [SerializeField] float inputDelay = 0.2f;
    [SerializeField] float eps = .05f;
    [SerializeField] CustomScrollRect scrollRect;
    private void OnEnable()
    {
        if (!PlayerPrefs.GetString("AccessibleBalls").Contains(id.ToString()))
            gameObject.SetActive(false);

        if(gameObject.activeInHierarchy)
            StartCoroutine(CheckIconPosition());

        if(!inputZone.isActiveAndEnabled)
            inputZone.enabled = true;
    }

    IEnumerator CheckIconPosition()
    {
        while(true)
        {
            if (id != PlayerPrefs.GetInt("ChosenBall") && inputZone.bounds.Contains(transform.position))
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

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
