using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the logic of traveling through levels
/// </summary>
public class LevelController : Controller
{
    private int currentLevel;
    public bool paused;
        
    protected override void Awake()
    {
        paused = false;
        currentLevel = CurrentScene.buildIndex;
        if(currentLevel != 0)
        {
            SetUpLevel();
        }

        title = GameTypeTitle.LEVEL;
        base.Awake();

        EventService.Instance.RegisterEventHandler(EventType.Pause, PauseLevel);
        EventService.Instance.RegisterEventHandler(EventType.Home, LoadMainMenu);
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
        LoadScene(0);
    }

    /// <summary>
    /// Loads in the scene at the given index; set in the build settings.
    /// TODO: create copy as scriptable object maybe
    /// </summary>
    /// <param name="buildIndex">Index of the scene according to build settings</param>
    public void LoadScene(int buildIndex)
    {
        EventService.Instance.HandleEvents(EventType.StartLevel);
        GameController.Instance.UnregisterTypes();
        SceneManager.LoadScene(buildIndex);

        if (buildIndex != 0)
        {
            SetUpLevel();
        }
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
        if (!paused) {
            paused = true;
            Time.timeScale = 0;
            //TODO: create pause menu
        }  else if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            //TODO: remove pause menu
        }
    }
}
