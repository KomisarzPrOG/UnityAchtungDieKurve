public class ReverseKeyBinds : PowerUp
{
    public float duration = 5f;

    private void Awake()
    {
        duration = settings.reverseKeyBindsDuration;
    }

    protected override void ApplySelf(Head player)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyToOthers(Head collector)
    {
        foreach (Head player in playerList)
        {
            if(player != collector && player.isAlive)
                player.StartCoroutine(player.ReverseKeyBinds(duration));
        }
    }

    protected override void ApplyGlobal(Head collector)
    {
        throw new System.NotImplementedException();
    }
}
