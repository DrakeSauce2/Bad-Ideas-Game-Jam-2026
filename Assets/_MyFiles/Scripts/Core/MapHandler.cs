using UnityEngine;
using UnityEngine.SceneManagement;

public static class MapNameUtility
{
    public static string GetMainMenuKey() => "Main_Menu";

    public static string GetFirstLevelKey() => "First_Level";

}

public class MapHandler : MonoBehaviour
{
    public static MapHandler Instance { get; private set; }

    public void SetMapInstance(MapHandler mapHandler)
    {
        if(Instance != null && Instance != mapHandler)
        {
            Destroy(gameObject);
            return;
        }

        Instance = mapHandler;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMap(string mapKey)
    {
        if(GameManager.Instance.IsGamePaused)
        {
            GameManager.Instance.ResumeGame();
        }
        
        SceneManager.LoadScene(mapKey);
    }
}
