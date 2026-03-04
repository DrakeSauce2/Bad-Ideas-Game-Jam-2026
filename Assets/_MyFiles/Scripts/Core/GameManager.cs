using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private MapHandler mapHandlerPrefab = null;

    public delegate void OnGamePaused(bool isPaused);
    public event OnGamePaused onGamePaused;

    private bool isGamePaused = false;
    public bool IsGamePaused => isGamePaused;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateStaticInstances();
    }

    private void CreateStaticInstances()
    {
        if(mapHandlerPrefab != null)
        {
            MapHandler mapHandler = Instantiate(mapHandlerPrefab); 
            mapHandler.SetMapInstance(mapHandler);
        }
    }

    public void PauseGame()
    {
        onGamePaused?.Invoke(true);
        isGamePaused = true;

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        onGamePaused?.Invoke(false);
        isGamePaused = false;

        Time.timeScale = 1f;
    }
}
