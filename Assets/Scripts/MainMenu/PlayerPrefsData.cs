using System.Collections.Generic;

public static class PlayerPrefsData
{
    public static List<PlayerConfig> players = new List<PlayerConfig>();
    
    public static void Save(List<PlayerConfig> input)
    {
        players = input;
    }
}
