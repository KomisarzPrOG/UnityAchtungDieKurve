using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;

    public class PlayerScore
    {
        public int playerId;
        public int score;
    }

    public List<PlayerScore> playerScores = new List<PlayerScore>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetUpPlayerScores()
    {
        if(playerScores.Count > 0) return;

        List<Head> players = new List<Head>();
        players = GameManager.Instance.GetAllPlayers();

        foreach(Head head in players)
        {
            playerScores.Add(new PlayerScore { playerId = head.id, score = 0 });
        }
    }

    public void OnPlayerDeath(int deadPlayerId)
    {
        foreach(PlayerScore p in playerScores)
        {
            if(p.playerId != deadPlayerId)
            {
                Head player = GameManager.Instance.GetPlayer(p.playerId);

                if (player != null && player.isAlive)
                    p.score++;
            }
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (PlayerScore p in playerScores)
        {
            Head player = GameManager.Instance.GetPlayer(p.playerId);

            if(player != null)
            {
                Debug.Log($"Gracz {player.Name}: {p.score} punktów.");
            }
        }
    }
}
