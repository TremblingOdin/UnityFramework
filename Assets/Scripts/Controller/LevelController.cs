﻿using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the logic of traveling through levels
/// </summary>
public class LevelController : MonoBehaviour
{
    public const GameTypeTitle title = GameTypeTitle.LEVEL;
    private int currentLevel;
    public bool paused;

    private void Awake()
    {
        paused = false;
        currentLevel = this.CurrentScene.buildIndex;
        if(currentLevel != 0)
        {
            SetUpLevel();
        }
        GameController.Instance.RegisterType(this, title, false);
        EventService.Instance.RegisterEventHandler(EventType.Pause, PauseLevel);
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
            this.SetUpLevel();
        }
    }

    /// <summary>
    /// Restart the current level
    /// </summary>
    public void RestartLevel()
    {
        GameController.Instance.UnregisterTypes();
        SceneManager.LoadScene(this.CurrentScene.buildIndex);
    }

    /// <summary>
    /// Initialize the necessary variables and data for the level
    /// </summary>
    public void SetUpLevel()
    {
        
    }

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