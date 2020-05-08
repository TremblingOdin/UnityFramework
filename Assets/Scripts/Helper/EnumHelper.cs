using UnityEngine;

public static class EnumHelper
{
    /// <summary>
    /// Converts KeyFunction to UserInput if possible, if not returns null
    /// </summary>
    /// <param name="value">KeyFunction to convert</param>
    /// <returns>If possible returns corresponding UserInput</returns>
    public static Player.UserInput? FunctionToInput(this KeyboardToEventHelper.KeyFunction value)
    {
        switch(value)
        {
            case KeyboardToEventHelper.KeyFunction.ATTACK:
                return Player.UserInput.ATTACK;
            case KeyboardToEventHelper.KeyFunction.DASH:
                return Player.UserInput.DASH;
            case KeyboardToEventHelper.KeyFunction.INTERACT:
                return Player.UserInput.INTERACT;
            case KeyboardToEventHelper.KeyFunction.JUMP:
                return Player.UserInput.JUMP;
            case KeyboardToEventHelper.KeyFunction.MOVEDOWN:
                return Player.UserInput.MOVEDOWN;
            case KeyboardToEventHelper.KeyFunction.MOVELEFT:
                return Player.UserInput.MOVELEFT;
            case KeyboardToEventHelper.KeyFunction.MOVERIGHT:
                return Player.UserInput.MOVERIGHT;
            case KeyboardToEventHelper.KeyFunction.MOVEUP:
                return Player.UserInput.MOVEUP;
            default:
                return null;
        }
    }

    /// <summary>
    /// Converts to KeyFunction
    /// All UserInputs are KeyFunctions so we don't need to worry about conversion
    /// </summary>
    /// <param name="value">UserInput to convert</param>
    /// <returns>Returns the corresponding KeyFunction</returns>
    public static KeyboardToEventHelper.KeyFunction InputToFunction(this Player.UserInput value)
    {
        switch(value)
        {
            case Player.UserInput.ATTACK:
                return KeyboardToEventHelper.KeyFunction.ATTACK;
            case Player.UserInput.DASH:
                return KeyboardToEventHelper.KeyFunction.DASH;
            case Player.UserInput.INTERACT:
                return KeyboardToEventHelper.KeyFunction.INTERACT;
            case Player.UserInput.JUMP:
                return KeyboardToEventHelper.KeyFunction.JUMP;
            case Player.UserInput.MOVEDOWN:
                return KeyboardToEventHelper.KeyFunction.MOVEDOWN;
            case Player.UserInput.MOVELEFT:
                return KeyboardToEventHelper.KeyFunction.MOVELEFT;
            case Player.UserInput.MOVERIGHT:
                return KeyboardToEventHelper.KeyFunction.MOVERIGHT;
            case Player.UserInput.MOVEUP:
                return KeyboardToEventHelper.KeyFunction.MOVEUP;
            default:
                return KeyboardToEventHelper.KeyFunction.NONE;
        }
    }

    /// <summary>
    /// Converts to UserInput
    /// Many Upgrades have no functionality so in that case return null
    /// </summary>
    /// <param name="value">Upgrade to convert</param>
    /// <returns>Returns the corresponding Input for an UpgradeType</returns>
    public static Player.UserInput? UpgradeToInput(this UpgradeType value)
    {
        switch(value)
        {
            case UpgradeType.DASH:
                return Player.UserInput.DASH;
            default:
                return null;
        }
    }

    /// <summary>
    /// Converts to UpgradeType
    /// Not all movements are linked to an upgrade so in that case return null
    /// </summary>
    /// <param name="value">Input to convert</param>
    /// <returns>Returns the corresponding UpgradeType</returns>
    public static UpgradeType? InputToUpgrade(this Player.UserInput? value)
    {
        switch (value)
        {
            case Player.UserInput.DASH:
                return UpgradeType.DASH;
            default:
                return null;
        }
    }
}
