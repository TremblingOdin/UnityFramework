using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class KeyboardToEventHelper : MonoBehaviour
{
    public const GameTypeTitle title = GameTypeTitle.KEYBOARD;

    public enum KeyFunction
    {
        ATTACK, INTERACT, JUMP, MOVEDOWN, MOVELEFT, MOVERIGHT, MOVEUP, PAUSE, NONE
    }

    private Dictionary<KeyCode, EventType> keyboardToEvent;

    private void Awake()
    {
        StreamReader sr = new StreamReader(GameController.controlSettingsPath);

        string line;
        //The organization of the file is:
        //variableName: Key
        while ((line = sr.ReadLine()) != null)
        {
            string[] lineArr = line.Split(GameController.Instance.Delimiter);
            if(Enum.Parse(typeof(KeyFunction), lineArr[0]) as KeyFunction? != null)
            {
                KeyFunction key = (KeyFunction)Enum.Parse(typeof(KeyFunction), lineArr[0]);

                if (codeMap.ContainsKey(lineArr[1])) {
                    switch (key)
                    {
                        //PlayerFunctional
                        case KeyFunction.ATTACK:
                            keyboardToEvent.Add(codeMap[lineArr[1]], EventType.Attack);
                            GameController.Instance.StorePlayerData(codeMap[lineArr[1]], Player.UserInput.ATTACK);
                            break;
                        case KeyFunction.INTERACT:
                            GameController.Instance.StorePlayerData(codeMap[lineArr[1]], Player.UserInput.INTERACT);
                            break;
                        case KeyFunction.JUMP:
                            keyboardToEvent.Add(codeMap[lineArr[1]], EventType.Jump);
                            GameController.Instance.StorePlayerData(codeMap[lineArr[1]], Player.UserInput.JUMP);
                            break;
                        case KeyFunction.MOVEDOWN:
                            GameController.Instance.StorePlayerData(codeMap[lineArr[1]], Player.UserInput.MOVEDOWN);
                            break;
                        case KeyFunction.MOVELEFT:
                            GameController.Instance.StorePlayerData(codeMap[lineArr[1]], Player.UserInput.MOVELEFT);
                            break;
                        case KeyFunction.MOVERIGHT:
                            GameController.Instance.StorePlayerData(codeMap[lineArr[1]], Player.UserInput.MOVERIGHT);
                            break;
                        case KeyFunction.MOVEUP:
                            GameController.Instance.StorePlayerData(codeMap[lineArr[1]], Player.UserInput.MOVEUP);
                            break;

                        //GameFunctional
                        case KeyFunction.PAUSE:
                            keyboardToEvent.Add(codeMap[lineArr[1]], EventType.Pause);
                            break;
                        default:
                            break;
                    }
                }


            } else
            {
                continue;
            }
        }


        GameController.Instance.RegisterType(this, title, false);
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode kc in keyboardToEvent.Keys)
        {
            if (Input.GetKeyDown(kc))
            {
                EventService.Instance.HandleEvents(keyboardToEvent[kc]);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            EventService.Instance.HandleEvents(EventType.Click);
        }
    }

    //Key to Keycode conversion dictionary
    public readonly Dictionary<string, KeyCode> codeMap = new Dictionary<string, KeyCode>()
    {
        //Common Special Keys
        { "Backspace", KeyCode.Backspace },
        { "Delete", KeyCode.Delete },
        { "Tab", KeyCode.Tab },
        { "Return", KeyCode.Return },
        { "Pause", KeyCode.Pause },
        { "Escape", KeyCode.Escape },
        { "Space", KeyCode.Space },
        
        //Number/Key pad
        { "Keypad1", KeyCode.Keypad1 },
        { "Keypad2", KeyCode.Keypad2 },
        { "Keypad3", KeyCode.Keypad3 },
        { "Keypad4", KeyCode.Keypad4 },
        { "Keypad5", KeyCode.Keypad5 },
        { "Keypad6", KeyCode.Keypad6 },
        { "Keypad7", KeyCode.Keypad7 },
        { "Keypad8", KeyCode.Keypad8 },
        { "Keypad9", KeyCode.Keypad9 },
        { "Keypad0", KeyCode.Keypad0 },
        { "Keypad.", KeyCode.KeypadPeriod },
        { "Keypad/", KeyCode.KeypadDivide },
        { "Keypad*", KeyCode.KeypadMultiply },
        { "Keypad-", KeyCode.KeypadMinus },
        { "Keypad+", KeyCode.KeypadPlus },
        { "KeypadReturn", KeyCode.KeypadEnter },
        { "KeypadEquals", KeyCode.KeypadEquals },
        
        //Arrow Keys
        { "Up", KeyCode.UpArrow },
        { "Down", KeyCode.DownArrow },
        { "Left", KeyCode.LeftArrow },
        { "Right", KeyCode.RightArrow },
        
        //Key cluster above arrows
        { "Inset", KeyCode.Insert },
        { "Home", KeyCode.Home },
        { "End", KeyCode.End },
        { "PageUp", KeyCode.PageUp },
        { "PageDown", KeyCode.PageDown },

        //F keys
        { "F1", KeyCode.F1 },
        { "F2", KeyCode.F2 },
        { "F3", KeyCode.F3 },
        { "F4", KeyCode.F4 },
        { "F5", KeyCode.F5 },
        { "F6", KeyCode.F6 },
        { "F7", KeyCode.F7 },
        { "F8", KeyCode.F8 },
        { "F9", KeyCode.F9 },
        { "F10", KeyCode.F10 },
        { "F11", KeyCode.F11 },
        { "F12", KeyCode.F12 },

        //Number keys above the alphabet keys
        { "A1", KeyCode.Alpha1 },
        { "A2", KeyCode.Alpha2 },
        { "A3", KeyCode.Alpha3 },
        { "A4", KeyCode.Alpha4 },
        { "A5", KeyCode.Alpha5 },
        { "A6", KeyCode.Alpha6 },
        { "A7", KeyCode.Alpha7 },
        { "A8", KeyCode.Alpha8 },
        { "A9", KeyCode.Alpha9 },
        { "A0", KeyCode.Alpha0 },

        //The rest of the special symbols
        { "!", KeyCode.Exclaim },
        { "\"", KeyCode.DoubleQuote },
        { "#", KeyCode.Hash },
        { "$", KeyCode.Dollar },
        { "%", KeyCode.Percent },
        { "&", KeyCode.Ampersand },
        { "'", KeyCode.Quote },
        { "{", KeyCode.LeftParen },
        { "}", KeyCode.RightParen },
        { "*", KeyCode.Asterisk },
        { "+", KeyCode.Plus },
        { ",", KeyCode.Comma },
        { "-", KeyCode.Minus },
        { ".", KeyCode.Period },
        { "/", KeyCode.Slash },
        { ":", KeyCode.Colon },
        { ";", KeyCode.Semicolon },
        { "<", KeyCode.Less },
        { ">", KeyCode.Greater },
        { "=", KeyCode.Equals },
        { "?", KeyCode.Question },
        { "@", KeyCode.At },
        { "[", KeyCode.LeftBracket },
        { "]", KeyCode.RightBracket },
        { "\\", KeyCode.Backslash },
        { "^", KeyCode.Caret },
        { "_", KeyCode.Underscore },
        { "`", KeyCode.BackQuote },
        { "|", KeyCode.Pipe },
        { "~", KeyCode.Tilde },

        //Lock Keys
        { "Num", KeyCode.Numlock },
        { "Cap", KeyCode.CapsLock },
        { "Scroll", KeyCode.ScrollLock },

        //Whatever Shift, Control, and Alt are considered
        { "LShift", KeyCode.LeftShift },
        { "RShift", KeyCode.RightShift },
        { "LCTL", KeyCode.LeftControl },
        { "RCTL", KeyCode.RightControl },
        { "LAlt", KeyCode.LeftAlt },
        { "RAlt", KeyCode.RightAlt },

        //Letters
        { "A", KeyCode.A },
        { "B", KeyCode.B },
        { "C", KeyCode.C },
        { "D", KeyCode.D },
        { "E", KeyCode.E },
        { "F", KeyCode.F },
        { "G", KeyCode.G },
        { "H", KeyCode.H },
        { "I", KeyCode.I },
        { "J", KeyCode.J },
        { "K", KeyCode.K },
        { "L", KeyCode.L },
        { "M", KeyCode.M },
        { "N", KeyCode.N },
        { "O", KeyCode.O },
        { "P", KeyCode.P },
        { "Q", KeyCode.Q },
        { "R", KeyCode.R },
        { "S", KeyCode.S },
        { "T", KeyCode.T },
        { "U", KeyCode.U },
        { "V", KeyCode.V },
        { "W", KeyCode.W },
        { "X", KeyCode.X },
        { "Y", KeyCode.Y },
        { "Z", KeyCode.Z },
        
        //Mouse
        { "Mouse0", KeyCode.Mouse0 },
        { "Mouse1", KeyCode.Mouse1 },
        { "Mouse2", KeyCode.Mouse2 },
        { "Mouse3", KeyCode.Mouse3 },
        { "Mouse4", KeyCode.Mouse4 },
        { "Mouse5", KeyCode.Mouse5 },
        { "Mouse6", KeyCode.Mouse6 }
    };
}
