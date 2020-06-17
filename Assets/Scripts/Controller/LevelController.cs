using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Menu types to assign to one's self
/// </summary>
public enum Menus
{
    Main, Pause, GameOver
}

/// <summary>
/// Handles the logic of traveling through levels
/// </summary>
public class LevelController : Controller
{
    public static int CurrentLevel { get; private set; }
    public static bool NewMusic { get; set; } = true;
    public static bool Paused { get; set; } = false;

    public static LevelController Instance { get; private set; } = new LevelController();

    public GameObject PauseMenu { get; set; }
    public GameObject GameOverMenu { get; set; }

    public GameData PlayerState;

    private LevelController()
    {
        if(Instance != null)
        {
            return;
        }

        title = GameTypeTitle.LEVEL;

        EventService.Instance.RegisterEventHandler(EventType.Pause, PauseLevel);
        EventService.Instance.RegisterEventHandler(EventType.Home, LoadMainMenu);

        //JunkData to initiate the gameData
        PlayerState = new GameData();

        PlayerState.Upgrades = new Dictionary<UpgradeType, int>();

        CurrentLevel = CurrentScene.buildIndex;
    }

    protected override void SceneLoaded(Scene s, LoadSceneMode lsm)
    {
        Debug.Log("loaded");
        SetUpLevel();
    }

    /// <summary>
    /// Current level running.
    /// Can get the build index with .buildIndex
    /// </summary>
    public Scene CurrentScene
    {
        get { return SceneManager.GetActiveScene(); }
    }

    /// <summary>
    /// Loads the main menu scene, which will be the first scene in the build index.
    /// </summary>
    public void LoadMainMenu()
    {
        LoadScene(0, true);
    }

    /// <summary>
    /// Loads in the scene at the given index; set in the build settings.
    /// TODO: create copy as scriptable object maybe
    /// </summary>
    /// <param name="buildIndex">Index of the scene according to build settings</param>
    public void LoadScene(int buildIndex, bool newMusic)
    {
        EventService.Instance.HandleEvents(EventType.StartLevel);

        if (newMusic)
        {
            AudioController.Instance.AudioObject.UnInvoke();
        }

        GameController.Instance.UnregisterTypes();
        SceneManager.LoadScene(buildIndex);

        SetUpLevel();
    }

    /// <summary>
    /// Restart the current level
    /// </summary>
    public void RestartLevel()
    {
        GameController.Instance.UnregisterTypes();
        SceneManager.LoadScene(CurrentScene.buildIndex);
    }

    /// <summary>
    /// Initialize the necessary variables and data for the level
    /// </summary>
    public void SetUpLevel()
    {
        
    }

    /// <summary>
    /// Stops time scale and opens pause menu
    /// </summary>
    public void PauseLevel()
    {
        if (!Paused) {
            Paused = true;
            Time.timeScale = 0;
            //TODO: create pause menu
        }  else if (Paused)
        {
            Paused = false;
            Time.timeScale = 1;
            //TODO: remove pause menu
        }
    }
}
