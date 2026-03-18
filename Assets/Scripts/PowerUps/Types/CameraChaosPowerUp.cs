public class CameraChaosPowerUp : PowerUp
{
    public float duration = 8f;

    private void Awake()
    {
        duration = settings.cameraChaosDuration;
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
        FindObjectOfType<CameraChaos>().ActivateChaos(duration);
    }
}
