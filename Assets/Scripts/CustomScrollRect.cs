using UnityEngine;
using UnityEngine.UI;

public class CustomScrollRect : MonoBehaviour
{
    public void ScrollContent(float delta)
    {
        ScrollRect rect = GetComponent<ScrollRect>();

        if (rect == null)
        {
            Debug.LogError("There is no SrollRect");
            return;
        }

        rect.horizontalNormalizedPosition += delta;

    }
}
