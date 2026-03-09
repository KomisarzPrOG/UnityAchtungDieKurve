using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerConfig
{
    public int playerID;
    public string playerName;
    public Material playerMaterial;

    public KeyCode leftKey = KeyCode.None;
    public KeyCode rightKey = KeyCode.None;

    public TMP_Text leftKeyText;
    public TMP_Text rightKeyText;

    public PlayerConfig(int id, string name, Material material, TMP_Text left, TMP_Text right)
    {
        playerID = id;
        playerName = name;
        playerMaterial = material;
        leftKeyText = left;
        rightKeyText = right;
    }
}
