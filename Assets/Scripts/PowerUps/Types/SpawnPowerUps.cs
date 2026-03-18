using UnityEngine;

public class SpawnPowerUps : PowerUp
{
    [Tooltip("Notice: this number is for some reason multiplied by 2")]
    public int powerUpsCount = 2;

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
        for (int i = 0; i < powerUpsCount; i++)
            PowerUpSpawner.Instance.SpawnPowerUp(true);
    }
}
