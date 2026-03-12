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

        GetEnabledPowerUps();
        CalculateTotalWeight();
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void GetEnabledPowerUps()
    {
        var settings = GameSettings.Instance;
        if (settings == null)
        {
            Debug.LogError("GameSettings not found!");
            return;
        }
        Debug.Log($"speedBoostEnabled: {settings.speedBoostEnabled}");

        if(!settings.powerUpsEnabled)
        {
            powerUpPrefabs.Clear();
            return;
        }

        var enabled = new List<WeightedPowerUp>();

        foreach(var p in powerUpPrefabs)
        {
            var powerUp = p.prefab.GetComponent<PowerUp>();
            if (powerUp == null) continue;

            if (powerUp is SpeedBoost && !settings.speedBoostEnabled) continue;
            else if (powerUp is SpeedBoost) p.weight = settings.speedBoostWeight;

            if (powerUp is SlowDown && !settings.slowDownEnabled) continue;
            else if (powerUp is SlowDown) p.weight = settings.slowDownWeight;

            if (powerUp is Grow && !settings.growEnabled) continue;
            else if (powerUp is Grow) p.weight = settings.growWeight;

            if (powerUp is Shrink && !settings.shrinkEnabled) continue;
            else if (powerUp is Shrink) p.weight = settings.shrinkWeight;

            if (powerUp is MazeMove && !settings.mazeMoveEnabled) continue;
            else if (powerUp is MazeMove) p.weight = settings.mazeMoveWeight;

            if (powerUp is PhaseWalk && !settings.phaseWalkEnabled) continue;
            else if (powerUp is PhaseWalk) p.weight = settings.phaseWalkWeight;

            if (powerUp is PlayerWrap && !settings.playerWrapEnabled) continue;
            else if (powerUp is PlayerWrap) p.weight = settings.playerWrapWeight;

            if (powerUp is ReverseKeyBinds && !settings.reverseKeyBindsEnabled) continue;
            else if (powerUp is ReverseKeyBinds) p.weight = settings.reverseKeyBindsWeight;

            if (powerUp is ClearTails && !settings.clearTailsEnabled) continue;
            else if (powerUp is ClearTails) p.weight = settings.clearTailsWeight;

            if (powerUp is CameraChaosPowerUp && !settings.cameraChaosEnabled) continue;
            else if (powerUp is CameraChaosPowerUp) p.weight = settings.cameraChaosWeight;

            if (powerUp is RandomPowerUp && !settings.randomPowerUpEnabled) continue;
            else if (powerUp is RandomPowerUp) p.weight = settings.randomPowerUpWeight;

            if (powerUp is SpawnPowerUps && !settings.spawnPowerUpsEnabled) continue;
            else if (powerUp is SpawnPowerUps) p.weight = settings.spawnPowerUpsWeight;

            if (powerUp is WrappedBorders && !settings.wrappedBordersEnabled) continue;
            else if (powerUp is WrappedBorders) p.weight = settings.wrappedBordersWeight;

            if (powerUp is ColorChaos && !settings.colorChaosEnabled) continue;
            else if (powerUp is ColorChaos) p.weight = settings.colorChaosWeight;

            enabled.Add(p);
        }

        powerUpPrefabs = enabled;
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