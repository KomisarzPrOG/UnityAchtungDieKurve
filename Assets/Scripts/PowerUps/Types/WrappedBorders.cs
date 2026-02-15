using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappedBorders : PowerUp
{
    public float duration = 10f;

    protected override void ApplySelf(Head player)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyToOthers(Head collector)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyGlobal()
    {
        Border border = GameObject.Find("GameBorder").GetComponent<Border>();

        border.StartCoroutine(border.WrappedBorders(duration));
    }
}
