using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;

    [SerializeField] Transform scoreBoardParent;
    [SerializeField] GameObject scoreEntryPrefab;

    List<GameObject> spawnedEntries = new List<GameObject>();

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

        SortScores();
        UpdateUI();
        DebugDump();
    }

    void SortScores()
    {
        playerScores.Sort((a, b) =>
        {
            int scoreCompare = b.score.CompareTo(a.score);

            if(scoreCompare != 0)
                return scoreCompare;

            return a.playerId.CompareTo(b.playerId);
        });
    }

    public void UpdateUI()
    {
        foreach(var entry in spawnedEntries)
            Destroy(entry);

        spawnedEntries.Clear();

        for(int i = 0; i < playerScores.Count; i++)
        {
            PlayerScore p = playerScores[i];
            Head head = GameManager.Instance.GetPlayer(p.playerId);

            if(head == null) continue;

            GameObject entryObject = Instantiate(scoreEntryPrefab, GameObject.Find("Content").transform);
            ScoreEntryUI entryUI = entryObject.GetComponent<ScoreEntryUI>();

            entryUI.Set(head.Name, p.score, head.playerColor);

            spawnedEntries.Add(entryObject);
        }
    }

    public void DebugDump()
    {
        string output = "==TABELA WYNIKÓW==\n";

        int i = 1;
        foreach (PlayerScore p in playerScores)
        {
            Head player = GameManager.Instance.GetPlayer(p.playerId);

            if(player != null)
            {
                output += $"\t{i}. {player.Name}: {p.score} punktów.\n";
                i++;
            }
        }

        Debug.Log(output);
    }
}
