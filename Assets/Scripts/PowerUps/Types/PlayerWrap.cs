using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrap : PowerUp
{
    float duration = 8f;

    protected override void ApplySelf(Head player)
    {
        player.StartCoroutine(player.PlayerWarp(duration));
    }

    protected override void ApplyToOthers(Head collector)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyGlobal(Head collector)
    {
        throw new System.NotImplementedException();
    }
}
