using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterials : MonoBehaviour
{
    public static PlayerMaterials Instance;

    [SerializeField] Material red, pink, green, gray, orange, aqua;

    void Awake() => Instance = this;

    static Dictionary<Color, Material> colorToMaterial;

    void Start()
    {
        colorToMaterial = new()
        {
            { HexColor("FF0000"), red },
            { HexColor("FF00D6"), pink },
            { HexColor("00FF00"), green },
            { HexColor("C8C8C8"), gray },
            { HexColor("FF7F00"), orange },
            { HexColor("00FFFF"), aqua }
        };
    }

    public Material GetMaterial(Color color) => colorToMaterial.TryGetValue(color, out var mat) ? mat : null;

    public Color GetRandomColor(Color exception)
    {
        List<Color> colors = new(colorToMaterial.Keys);
        colors.Remove(exception);
        return colors[Random.Range(0, colors.Count)];
    }

    static Color HexColor(string hex)
    {
        ColorUtility.TryParseHtmlString("#" + hex, out Color c);
        return c;
    }
}
