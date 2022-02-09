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
        if (!next)
            inputDelta *= -1.0f;

        rect.horizontalNormalizedPosition += inputDelta;

        Debug.LogError(inputDelta);
    }

    public float GetInputDelta()
    {
        return inputDelta;
    }

    public ScrollRect GetScrollRect()
    {
        return rect;
    }

    public float GetHorNormPos()
    {
        return rect.horizontalNormalizedPosition;
    }

    public void SetHorNormPos(float newPos)
    {
        rect.horizontalNormalizedPosition = newPos;
    }
}
