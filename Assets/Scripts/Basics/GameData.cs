using System.Collections.Generic;
using System;

[System.Serializable]
public struct GameData
{
    public int PlayerFile;
    public int Scene;
    public int PlayerHealth;
    public int Attack;
    public Dictionary<UpgradeType, int> Upgrades;
}

public static class GameDataHelper
{
    /// <summary>
    /// Override the ToString to generate a one line with GameController defined seperators
    /// </summary>
    /// <param name="value">GameData calling the function</param>
    /// <returns>JSON representation as a string</returns>
    public static string ToString(this GameData value)
    {
        string dataInfo = GameController.Instance.BeginClass.ToString();

        dataInfo += nameof(value.Scene) + GameController.Instance.Delimiter 
            + value.Scene + GameController.Instance.Segmenter;

        dataInfo += nameof(value.PlayerHealth) + GameController.Instance.Delimiter
            + value.PlayerHealth + GameController.Instance.Segmenter;

        dataInfo += nameof(value.Attack) + GameController.Instance.Delimiter
            + value.Attack + GameController.Instance.Segmenter;

        dataInfo += nameof(value.Upgrades) + GameController.Instance.BeginGroup;
        foreach(UpgradeType key in value.Upgrades.Keys)
        {
            dataInfo += Enum.GetName(typeof(UpgradeType), key) + GameController.Instance.Delimiter
                + value.Upgrades[key] + GameController.Instance.Segmenter;
        }
        dataInfo += GameController.Instance.EndGroup;

        dataInfo += GameController.Instance.EndClass;

        return dataInfo;
    }

    /// <summary>
    /// Generates a GameData with GameController defined separators
    /// </summary>
    /// <param name="value"></param>
    /// <param name="data"></param>
    public static void FromString(this GameData value, string data)
    {
        foreach(string field in data.Split(GameController.Instance.Segmenter))
        {
            if(field.Split(GameController.Instance.BeginGroup).Length > 1)
            {

            }
        }
    }
}
