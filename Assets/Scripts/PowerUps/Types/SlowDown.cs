using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : PowerUp
{
    public float speedMultiplier = 0.75f;
    public float duration = 4f;

    protected override void ApplySelf(Head player)
    {
        player.StartCoroutine(player.ModifySpeed(speedMultiplier, duration));
    }

    protected override void ApplyToOthers(Head collector)
    {
        foreach (Head player in playerList)
        {
            if (player != collector && player.isAlive)
                player.StartCoroutine(player.ModifySpeed(speedMultiplier, duration));
        }
    }

    protected override void ApplyGlobal()
    {
        throw new System.NotImplementedException();
    }
}