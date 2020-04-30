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
                Debug.Log("[Enum Helper] cannot handle UserInput value: " + value);
                return KeyboardToEventHelper.KeyFunction.NONE;
        }
    }
}
