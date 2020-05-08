using UnityEngine.SceneManagement;

public class Controller
{
    protected GameTypeTitle title = GameTypeTitle.NONE;
    

    protected Controller() { }

    /// <summary>
    /// When the enabled add scene listener
    /// </summary>
    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    /// <summary>
    /// For singletons this replaces Start
    /// </summary>
    protected virtual void SceneLoaded(Scene s, LoadSceneMode lsm)
    {

    }
}
