using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public List<GameObject> powerUpPrefabs;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float spawnInterval = 2.5f;
    public int spawnChance = 20;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPowerUp), 5f, spawnInterval);
    }

    void SpawnPowerUp()
    {
        if (Random.Range(0f, 100f) > spawnChance) return;

        int index = Random.Range(0, powerUpPrefabs.Count);
        Vector2 position = new Vector2(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y)
        );

        GameObject powerUpObject = Instantiate(powerUpPrefabs[index], position, Quaternion.identity);

        GameObject parent = GameObject.Find("PowerUps");
        if (parent != null)
            powerUpObject.transform.SetParent(parent.transform);

        PowerUp powerUp = powerUpObject.GetComponent<PowerUp>();
        if(powerUp != null)
            powerUp.RandomizeTargetType();
    }
}
