using UnityEngine;

public class GUIHandler : MonoBehaviour
{
    [SerializeField] WidgetBase playerHUD = null;
    [SerializeField] WidgetBase pauseMenu = null;

    private void Awake()
    {
        GameManager.Instance.onGamePaused += OnGamePaused;
    }

    private void Start()
    {
        OnGamePaused(false);
    }

    private void OnGamePaused(bool isPaused)
    {
        SetCursorState(isPaused);

        SetWidgetActive(pauseMenu, isPaused);
        SetWidgetActive(playerHUD, !isPaused);
    }

    private void SetCursorState(bool isActive)
    {
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    private void SetWidgetActive(WidgetBase widget, bool isActive)
    {
        if(widget != null)
        {
            widget.SetActive(isActive);
        }
    }
}
