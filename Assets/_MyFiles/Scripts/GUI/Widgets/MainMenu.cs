using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button quitBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(OnStartBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnQuitBtnClicked()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    
#endif

    }

    private void OnStartBtnClicked()
    {
        MapHandler.Instance.LoadMap(MapNameUtility.GetFirstLevelKey());
    }
}
