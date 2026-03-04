using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class WidgetBase : MonoBehaviour
{
    private CanvasGroup canvasGroup = null;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetActive(bool isActive)
    {
        if(canvasGroup != null)
        {
            canvasGroup.alpha = isActive ? 1f : 0f;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
    }
}
