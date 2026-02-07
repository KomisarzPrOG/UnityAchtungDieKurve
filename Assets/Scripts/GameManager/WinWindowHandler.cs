using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinWindowHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] borders;
    [SerializeField] SpriteRenderer background;
    [SerializeField] TMP_Text[] tMP_Texts;

    public void SetWindow(Color color, string name)
    {
        foreach(var b in borders)
            b.color = color;

        tMP_Texts[0].color = color;
        tMP_Texts[1].color = color;
        tMP_Texts[1].text = $"{name} won!";

        color.a = 16f/255f;
        background.color = color;
    }
}
