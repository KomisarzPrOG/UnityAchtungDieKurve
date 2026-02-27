using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaosPowerUp : PowerUp
{
    public float duration = 8f;

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
