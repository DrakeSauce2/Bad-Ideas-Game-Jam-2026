using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : WidgetBase
{
    [SerializeField] Button resumeBtn = null;
    [SerializeField] Button mainMenuBtn = null;

    private void OnEnable()
    {
        resumeBtn.onClick.AddListener(OnResumeBtnClicked);
        mainMenuBtn.onClick.AddListener(OnMainMenuBtnClicked);
    }

    void OnDisable()
    {
        resumeBtn.onClick.RemoveListener(OnResumeBtnClicked);
        mainMenuBtn.onClick.RemoveListener(OnMainMenuBtnClicked);
    }

    private void OnResumeBtnClicked()
    {
        GameManager.Instance.ResumeGame();
    }

    private void OnMainMenuBtnClicked()
    {
        MapHandler.Instance.LoadMap(MapNameUtility.GetMainMenuKey());
    }
}
