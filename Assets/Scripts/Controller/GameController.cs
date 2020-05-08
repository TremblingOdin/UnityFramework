using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using UnityEngine;
using System.Collections;

public enum GameTypeTitle
{
    AUDIO, BUILD, CAMERA, LEVEL, KEYBOARD, PLAYER, NONE
}

public enum UserSetting
{
    FULLSCREEN, QUALITY, RESOLUTION, VOLUME
}

/// <summary>
/// Singleton entity which handles connecting and registering the different logic engines in the game.
/// </summary>
[DisallowMultipleComponent]
public sealed class GameController
{
    public char Delimiter { get; } = ':';

    private Dictionary<GameTypeTitle, List<MonoBehaviour>> registeredTypes = new Dictionary<GameTypeTitle, List<MonoBehaviour>>();

    public Dictionary<KeyCode, Player.UserInput> PlayControl { get; }

    public Hashtable UserSettings { get; } = new Hashtable();

    //User settings file, it's going to get referenced a lot figured this would be the best place to put it
    public static readonly string userSettingsPath = "/playerSettings.dat";
    public static readonly string controlSettingsPath = "/controlSettings.dat";

    public static GameController Instance { get; } = new GameController();

    static GameController() { }

    private GameController()
    {
        foreach(UserSetting us in (UserSetting[])Enum.GetValues(typeof(UserSetting))) {
            UserSettings.Add(us, null);
        }

        PlayControl = new Dictionary<KeyCode, Player.UserInput>();
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

    /// <summary>
    /// Connects a KeyCode to a UserInput value and returns if it had to overwrite something
    /// </summary>
    /// <param name="KeyCode">The KeyCode to look for</param>
    /// <param name="UserInput">The type of user input to execute</param>
    /// <returns>Was a pre-existing value overwritten</returns>
    public bool StorePlayerData(KeyCode kc, Player.UserInput ui)
    {
        if (PlayControl.ContainsKey(kc))
        {
            PlayControl[kc] = ui;
            return true;
        }
        else
        {
            PlayControl.Add(kc, ui);
            return false;
        }
    }

    /// <summary>
    /// Goes through it's PlayControl dictionary and writes each key and value to a line in the control setting file
    /// </summary>
    /// <returns>Success of the save</returns>
    public bool SavePlayerData()
    {
        try
        {
            if (!File.Exists(Application.persistentDataPath + controlSettingsPath))
            {
                File.Create(Application.persistentDataPath + controlSettingsPath);
            }

            StreamWriter sw = new StreamWriter(Application.persistentDataPath + controlSettingsPath);

            foreach (KeyCode key in PlayControl.Keys)
            {
                sw.Write(Enum.GetName(typeof(KeyboardToEventHelper.KeyFunction), PlayControl[key].InputToFunction()) 
                    + Delimiter.ToString() 
                    + Enum.GetName(typeof(KeyCode), key) + sw.NewLine);
            }

            sw.Close();

            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    /// <summary>
    /// Goes through it's usersettings dictionary and writes each enum and value to a instance in the usersetting file
    /// </summary>
    /// <returns></returns>
    public bool SaveSettings()
    {
        try
        {
            if (!File.Exists(Application.persistentDataPath + userSettingsPath))
            {
                File.Create(Application.persistentDataPath + userSettingsPath);
            }

            StreamWriter sw = new StreamWriter(Application.persistentDataPath + userSettingsPath);

            foreach (UserSetting us in UserSettings.Keys)
            {
                if (UserSettings[us] != null)
                {
                    sw.Write(Enum.GetName(typeof(UserSetting), us) 
                        + Delimiter.ToString() 
                        + UserSettings[us].ToString() + sw.NewLine);
                }
            }

            sw.Close();

            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    /// <summary>
    /// Loads the usersettings dictionary with values from the settings file
    /// The file should be in a format such as:
    /// VOLUME:.5
    /// </summary>
    /// <returns></returns>
    public bool LoadSettings()
    {
        if(!File.Exists(Application.persistentDataPath + userSettingsPath))
        {
            return false;
        }

        StreamReader sr = new StreamReader(Application.persistentDataPath + userSettingsPath);

        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string settingType = line.Split(Delimiter)[0];
            UserSetting tempUS = (UserSetting)Enum.Parse(typeof(UserSetting), settingType);

            if (Enum.IsDefined(typeof(UserSetting), tempUS))
            {
                UserSettings[tempUS] = line.Split(Delimiter)[1];
            }
        }

        return true;
        
    }
}
