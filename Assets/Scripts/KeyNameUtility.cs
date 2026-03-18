using System.Collections.Generic;
using UnityEngine;

public static class KeyNameUtility
{
    static Dictionary<KeyCode, string> prettyNames = new()
    {
        // === NUMBERS ===
        { KeyCode.Alpha0, "0" },
        { KeyCode.Alpha1, "1" },
        { KeyCode.Alpha2, "2" },
        { KeyCode.Alpha3, "3" },
        { KeyCode.Alpha4, "4" },
        { KeyCode.Alpha5, "5" },
        { KeyCode.Alpha6, "6" },
        { KeyCode.Alpha7, "7" },
        { KeyCode.Alpha8, "8" },
        { KeyCode.Alpha9, "9" },

        // === NUMPAD ===
        { KeyCode.Keypad0, "Num 0" },
        { KeyCode.Keypad1, "Num 1" },
        { KeyCode.Keypad2, "Num 2" },
        { KeyCode.Keypad3, "Num 3" },
        { KeyCode.Keypad4, "Num 4" },
        { KeyCode.Keypad5, "Num 5" },
        { KeyCode.Keypad6, "Num 6" },
        { KeyCode.Keypad7, "Num 7" },
        { KeyCode.Keypad8, "Num 8" },
        { KeyCode.Keypad9, "Num 9" },

        // === OPERATORS / SYMBOLS ===
        { KeyCode.Slash, "/" },
        { KeyCode.KeypadDivide, "/" },

        { KeyCode.Backslash, "\\" },

        { KeyCode.Minus, "-" },
        { KeyCode.KeypadMinus, "-" },

        { KeyCode.Equals, "=" },
        { KeyCode.Plus, "+" },
        { KeyCode.KeypadPlus, "+" },

        { KeyCode.Asterisk, "*" },
        { KeyCode.KeypadMultiply, "*" },

        { KeyCode.Period, "." },
        { KeyCode.KeypadPeriod, "." },

        { KeyCode.Comma, "," },

        { KeyCode.Semicolon, ";" },
        { KeyCode.Quote, "'" },

        { KeyCode.LeftBracket, "[" },
        { KeyCode.RightBracket, "]" },

        { KeyCode.BackQuote, "`" },

        // === NAVIGATION ===
        { KeyCode.Return, "Enter" },
        { KeyCode.KeypadEnter, "NumEntr" },

        // === ARROWS ===
        { KeyCode.LeftArrow, "←" },
        { KeyCode.RightArrow, "→" },
        { KeyCode.UpArrow, "↑" },
        { KeyCode.DownArrow, "↓" },

        // === OTHER ===
        { KeyCode.LeftShift, "R Shift" },
        { KeyCode.RightShift, "L Shift" },
        { KeyCode.Backspace, "Backspc" }
    };

    public static string ToPretty(KeyCode key)
    {
        if(prettyNames.TryGetValue(key, out var name))
            return name;

        string raw = key.ToString();

        if (raw.StartsWith("Alpha"))
            return raw.Replace("Alpha", "");

        if (raw.StartsWith("Keypad"))
            return "Num " + raw.Replace("Keypad", "");

        return raw;
    }
}
