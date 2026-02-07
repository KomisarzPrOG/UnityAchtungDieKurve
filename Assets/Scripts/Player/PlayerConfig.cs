using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerConfig
{
    public int playerID;
    public string playerName;
    public Color playerColor;

    public KeyCode leftKey;
    public KeyCode rightKey;

    public TMP_Text leftKeyText;
    public TMP_Text rightKeyText;

    public PlayerConfig(int id, string name, Color color, TMP_Text left, TMP_Text right)
    {
        playerID = id;
        playerName = name;
        playerColor = color;
        leftKeyText = left;
        rightKeyText = right;
    }
}
