using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomPowerUp : PowerUp
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
        PowerUp randomPU = GetRandomPowerUp();
        if (randomPU == null) return;

        randomPU.RandomizeTargetType();
        randomPU.Activate(collector);
    }

    private PowerUp GetRandomPowerUp()
    {
        var list = PowerUpSpawner.Instance.powerUpPrefabs;

        if (list == null || list.Count == 0)
            return null;

        int index = Random.Range(0, list.Count);

        return list[index].GetComponent<PowerUp>();
    }
}
