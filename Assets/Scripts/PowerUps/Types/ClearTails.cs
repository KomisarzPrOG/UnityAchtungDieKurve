using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTails : PowerUp
{
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
        List<Head> playerList = GameManager.Instance.GetAllPlayers();

        foreach (Head player in playerList)
        {
            if (player != null)
            {
                player.tail.ResetTail();
            }
        }
    }
}
