using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WeightedPowerUp
    {
        public GameObject prefab;
        [Range(0f, 100f)]
        public float weight;
    }

    [Header("Settings")]
    public List<WeightedPowerUp> powerUpPrefabs;
    public Transform powerUpsParent;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float spawnInterval = 2.5f;
    [Range(0, 100)]
    public int spawnChance = 20;

    public static PowerUpSpawner Instance { get; private set; }

    private float totalWeight;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CalculateTotalWeight();
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void CalculateTotalWeight()
    {
        totalWeight = 0;
        foreach (var p in powerUpPrefabs) totalWeight += p.weight;
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            SpawnPowerUp();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnPowerUp(bool skipChance = false)
    {
        if (!skipChance && Random.Range(0f, 100f) > spawnChance) return;
        if (powerUpPrefabs == null || powerUpPrefabs.Count == 0) return;

        GameObject prefabToSpawn = GetWeightedRandomPrefab();
        if (prefabToSpawn == null) return;

        Vector2 position = new Vector2(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y)
        );

        // Tworzenie od razu pod rodzicem (wydajniejsze ni¿ SetParent póŸniej)
        GameObject powerUpObject = Instantiate(prefabToSpawn, position, Quaternion.identity, powerUpsParent);

        if (powerUpObject.TryGetComponent<PowerUp>(out var powerUp))
        {
            powerUp.RandomizeTargetType();
        }
    }

    private GameObject GetWeightedRandomPrefab()
    {
        float randomValue = Random.Range(0, totalWeight);
        float currentSum = 0;

        foreach (var p in powerUpPrefabs)
        {
            currentSum += p.weight;
            if (randomValue <= currentSum) return p.prefab;
        }

        return powerUpPrefabs[0].prefab;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2, (minBounds.y + maxBounds.y) / 2, 0);
        Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 0.1f);
        Gizmos.DrawWireCube(center, size);
    }
}