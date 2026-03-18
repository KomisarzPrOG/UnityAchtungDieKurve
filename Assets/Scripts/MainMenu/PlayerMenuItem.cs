using TMPro;
using UnityEngine;

public class PlayerMenuItem : MonoBehaviour
{
    public TMP_Text[] texts;
    public Material material;
    public int playerID;

    bool selected = false;

    public float timeBeforeNextCheck = 0;

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
    void Toggle()
    {
        if (selected)
            Deselect();
        else
            Select();
    }
    void Select()
    {
        selected = true;
        SetAlpha(255);
        PlayerSelectManager.Instance.SelectPlayer(playerID, texts[1].text.ToString(), material, texts[2], texts[3]);
    }

    void Deselect()
    {
        selected = false;
        SetAlpha(128);
        PlayerSelectManager.Instance.DeselectPlayer(playerID);
        texts[2].text = "";
        texts[3].text = "";
    }

    private void OnMouseDown()
    {
        Toggle();
    }

    private void Update()
    {
        if (PlayerSelectManager.Instance.IsWaitingForInput()) return;
        HandleNumberInput();
    }

    void HandleNumberInput()
    {
        int pressedNum = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) pressedNum = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) pressedNum = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) pressedNum = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) pressedNum = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5)) pressedNum = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha6)) pressedNum = 6;

        if (pressedNum == playerID)
            Toggle();
    }
}
