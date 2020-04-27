using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public enum GameTypeTitle
{
    AUDIO, CAMERA, LEVEL, KEYBOARD
}

public enum UserSetting
{
    VOLUME
}

/// <summary>
/// Singleton entity which handles connecting and registering the different logic engines in the game.
/// </summary>
[DisallowMultipleComponent]
public sealed class GameController
{
    private static readonly GameController instance = new GameController();

    private Dictionary<GameTypeTitle, List<MonoBehaviour>> registeredTypes = new Dictionary<GameTypeTitle, List<MonoBehaviour>>();
    private Dictionary<UserSetting, object> userSettings = new Dictionary<UserSetting, object>();
    
    public Dictionary<UserSetting, object> UserSettings
    {
        get { return userSettings; }
    }

    //User settings file, it's going to get referenced a lot figured this would be the best place to put it
    public static readonly string userSettingsPath = "Assets/Resources/Files/playerSettings.txt";

    static GameController() { }

    private GameController()
    {

    }

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Get a reference to the registered type requested. Returns first found.
    /// Only use with non-repeatable types
    /// </summary>
    /// <typeparam name="T">The MonoBehaviour type to get</typeparam>
    /// <param name="title">The key to search for among the dictionary</param>
    /// <returns>A reference to the requested type, if the type isn't registered returns null</returns>
    public T GetType<T>(GameTypeTitle title) where T:MonoBehaviour
    {
        if(this.registeredTypes.ContainsKey(title))
        {
            this.registeredTypes.TryGetValue(title, out List<MonoBehaviour> value);

            return (T)value[0];
            
        }

        return null;
    }

    /// <summary>
    /// Get a reference to the list of registered type requested.
    /// Only use with repeatable types
    /// </summary>
    /// <typeparam name="T">The Monobehaviour types to get</typeparam>
    /// <param name="title">The key to search for among the dictionary</param>
    /// <returns>A list of references to the requested type if the type isn't registered returns null</returns>
    public List<MonoBehaviour> GetTypes<T>(GameTypeTitle title) where T:MonoBehaviour
    {
        if (this.registeredTypes.ContainsKey(title))
        {
            this.registeredTypes.TryGetValue(title, out List<MonoBehaviour> value);

            return value;

        }

        return null;
    }

    /// <summary>
    /// Register the given type so that only one of the type with the associated key exists at run time.
    /// Call function in Awake()
    /// </summary>
    /// <param name="mb">The type to register</param>
    /// <param name="title">The key to associate it with</param>
    public void RegisterType(MonoBehaviour mb, GameTypeTitle title, bool repeatable)
    {
        TypeInfo type = mb.GetType().GetTypeInfo();

        if (this.registeredTypes.ContainsKey(title) && !repeatable)
        {
            Debug.LogWarning("[GameController] attempted to register duplicate keys for type: " + title);
        }
        else if (!this.registeredTypes.ContainsKey(title)) {
            List<MonoBehaviour> lmb = new List<MonoBehaviour>();
            lmb.Add(mb);
            this.registeredTypes.Add(title, lmb);
        }
        else
        {
            this.registeredTypes.TryGetValue(title, out List<MonoBehaviour> list);
            list.Add(mb);
            Debug.Log("[GameController] Type registered: " + mb);
        }
    }

    /// <summary>
    /// Removes registered type with the given key
    /// </summary>
    /// <param name="title">Key of the registered type to remove</param>
    public void UnregisterType(GameTypeTitle title)
    {
        this.registeredTypes.Remove(title);
    }

    /// <summary>
    /// Removes all registered types
    /// </summary>
    public void UnregisterTypes()
    {
        this.registeredTypes.Clear();
    }

    /// <summary>
    /// Checks if the type is registered to the GameController.
    /// </summary>
    /// <returns>If type was registered</returns>
    public bool HasType(GameTypeTitle title)
    {
        return this.registeredTypes.ContainsKey(title);
    }
}
