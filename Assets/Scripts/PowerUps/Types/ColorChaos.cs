using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChaos : PowerUp
{
    public float duration = 8f;

    private void Awake()
    {
        duration = settings.colorChaosDuration;
    }

    protected override void ApplySelf(Head player)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyToOthers(Head collector)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyGlobal(Head collector)
    {
        foreach(var player in playerList)
            player.StartCoroutine(player.ColorChange(duration));
    }
}
