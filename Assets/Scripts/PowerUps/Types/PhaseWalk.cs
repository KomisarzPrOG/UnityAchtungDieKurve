using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseWalk : PowerUp
{
    public float duration = 5f;

    private void Awake()
    {
        duration = settings.phaseWalkDuration;
    }

    protected override void ApplySelf(Head player)
    {
        player.StartCoroutine(player.ActivatePhaseWalk(duration));
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
