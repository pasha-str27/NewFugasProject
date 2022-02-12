using UnityEngine;
using UnityEngine.UI;

public class CustomScrollRect : MonoBehaviour
{
    float inputDelta;
    ScrollRect rect;

    private void Start()
    {
        rect = GetComponent<ScrollRect>();

        if (rect == null)
        {
            Debug.LogError("There is no SrollRect");
            return;
        }

        inputDelta = 1.0f / (PlayerPrefs.GetString("AccessibleBalls").Length - 1.0f);
    }

    public void ScrollContent(bool next)
    {
        rect.horizontalNormalizedPosition += next ? inputDelta : -inputDelta;
    }

    public float GetInputDelta() => inputDelta;
    public ScrollRect GetScrollRect() => rect;
    public float GetHorNormPos() => rect.horizontalNormalizedPosition;
    public void SetHorNormPos(float newPos) => rect.horizontalNormalizedPosition = newPos;
}
