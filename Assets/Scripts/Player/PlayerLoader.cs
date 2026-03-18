using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject playerPrefab;

    private List<PlayerConfig> playerList = new List<PlayerConfig>();

    void Start()
    {
        playerList = PlayerPrefsData.players;

        LoadPlayers();
    }

    void LoadPlayers()
    {
        foreach(var player in playerList)
        {
            GameObject playerObject = Instantiate(playerPrefab, GameObject.Find("Players").transform);
            Head playerHead = playerObject.GetComponentInChildren<Head>();
            Tail playerTail = playerObject.GetComponentInChildren<Tail>();

            playerHead.SetHead(player.playerName, player.playerID, player.leftKey, player.rightKey, player.playerMaterial.color);
            playerTail.SetTail(player.playerMaterial);
        }
    }
}
