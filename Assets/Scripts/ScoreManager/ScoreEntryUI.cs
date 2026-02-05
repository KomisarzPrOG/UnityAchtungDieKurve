using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEntryUI : MonoBehaviour
{
    [SerializeField] TMP_Text playerNameText;
    [SerializeField] TMP_Text scoreText;

    public void Set(string name, int score, Color color)
    {
        playerNameText.text = name;
        scoreText.text = score.ToString();

        playerNameText.color = color;
        scoreText.color = color;
    }
}
