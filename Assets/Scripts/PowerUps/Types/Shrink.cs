using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : PowerUp
{
    public float duration = 8f;
    public float multiplier = 0.9f;

    protected override void ApplySelf(Head player)
    {
        player.StartCoroutine(player.ModifySize(multiplier, duration));
    }

    protected override void ApplyToOthers(Head collector)
    {
        foreach (Head player in playerList)
        {
            if (player != collector && player.isAlive)
                player.StartCoroutine(player.ModifySize(multiplier, duration));
        }
    }

    protected override void ApplyGlobal()
    {
        throw new System.NotImplementedException();
    }
}
