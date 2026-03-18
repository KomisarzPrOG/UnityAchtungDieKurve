public class SpeedBoost : PowerUp
{
    public float speedMultiplier = 1.25f;
    public float duration = 4f;

    private void Awake()
    {
        speedMultiplier = settings.speedBoostMultiplier;
        duration = settings.speedBoostDuration;
    }

    protected override void ApplySelf(Head player)
    {
        player.StartCoroutine(player.ModifySpeed(speedMultiplier, duration));
    }

    protected override void ApplyToOthers(Head collector)
    {
        foreach(Head player in playerList)
        {
            if(player != collector && player.isAlive)
                player.StartCoroutine(player.ModifySpeed(speedMultiplier, duration));
        }
    }

    protected override void ApplyGlobal(Head collector)
    {
        throw new System.NotImplementedException();
    }
}
