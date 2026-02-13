using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseKeyBinds : PowerUp
{
    public float duration = 5f;

    protected override void ApplySelf(Head player)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyToOthers(Head collector)
    {
        List<Head> playerList = GameManager.Instance.GetAllPlayers();

        foreach (Head player in playerList)
        {
            if(player != collector)
                player.StartCoroutine(player.ReverseKeyBinds(duration));
        }
    }

    protected override void ApplyGlobal()
    {
        throw new System.NotImplementedException();
    }
}
