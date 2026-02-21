using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMove : PowerUp
{
    public float duration = 8f;

    protected override void ApplySelf(Head player)
    {
        player.StartCoroutine(player.ActivateMazeMove(duration));
    }

    protected override void ApplyToOthers(Head collector)
    {
        foreach(Head player in playerList)
        {
            if(player != collector && player.isAlive)
                player.StartCoroutine(player.ActivateMazeMove(duration));
        }
    }

    protected override void ApplyGlobal()
    {
        throw new System.NotImplementedException();
    }
}
