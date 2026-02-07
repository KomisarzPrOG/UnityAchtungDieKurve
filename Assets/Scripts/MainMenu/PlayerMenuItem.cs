using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMenuItem : MonoBehaviour
{
    public TMP_Text[] texts;
    public int playerID;

    bool selected = false;

    void SetAlpha(byte a)
    {
        Color c = texts[0].color;
        c.a = a / 255f;

        foreach(var t in texts)
            t.color = c;
    }

    private void OnMouseEnter()
    {
        if (!selected)
            SetAlpha(128);
    }

    private void OnMouseExit()
    {
        if(!selected)
            SetAlpha(64);
    }

    private void OnMouseDown()
    {
        if (selected)
        {
            SetAlpha(128);
        }
        else
        {
            PlayerSelectManager.Instance.SelectPlayer(playerID, texts[1].ToString(), texts[1].color, texts[2], texts[3]);
            SetAlpha(255);
        }

        selected = !selected;
    }
}
