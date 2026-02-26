using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grow : PowerUp
{
    public float duration = 8f;
    public float multiplier = 1.05f;

    protected override void ApplySelf(Head player)
    {
        player.StartCoroutine(player.ModifySize(multiplier, duration));
    }

    protected override void ApplyToOthers(Head collector)
    {
        foreach(Head player in playerList)
        {
            if(player != collector && player.isAlive)
                player.StartCoroutine(player.ModifySize(multiplier, duration));
        }
    }

    protected override void ApplyGlobal()
    {
        throw new System.NotImplementedException();
    }
}
