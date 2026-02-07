using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerPrefsData
{
    public static List<PlayerConfig> players = new List<PlayerConfig>();
    
    public static void Save(List<PlayerConfig> input)
    {
        players = input;
    }
}
