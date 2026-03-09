using UnityEngine;

[CreateAssetMenu(menuName = "AchtungDieKurve/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    private static GameSettings _instance;
    public static GameSettings Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<GameSettings>("GameSettings");
            return _instance;
        }
    }

    private void OnEnable()
    {
        _instance = this;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetInstance()
    {
        _instance = null;
    }

    [Header("=== PLAYERS ===")]
    [Tooltip("Player speed")]
    public float playerSpeed = 1.5f;

    [Tooltip("Player turn speed")]
    public float playerTurnSpeed = 180f;

    [Tooltip("Player line width")]
    public float playerLineWidth = 0.2f;

    [Tooltip("Gap creation chance")]
    public float gapFrequency = 0.05f;

    [Tooltip("Size of gaps in line")]
    public float gapSize = 15f;

    [Header("=== POWER-UPS ===")]
    [Tooltip("Are power-ups active")]
    public bool powerUpsEnabled = true;

    [Header("Power-Up: Speed Boost")]
    public bool speedBoostEnabled = true;
    [Tooltip("Speed multiplier")]
    public float speedBoostMultiplier = 1.25f;
    [Tooltip("Duration (seconds)")]
    public float speedBoostDuration = 6f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float speedBoostWeight = 60f;

    [Header("Power-Up: Slow Down")]
    public bool slowDownEnabled = true;
    [Tooltip("Speed multiplier")]
    public float slowDownMultiplier = 0.75f;
    [Tooltip("Duration (seconds)")]
    public float slowDownDuration = 6f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float slowDownWeight = 60f;

    [Header("Power-Up: Grow")]
    public bool growEnabled = true;
    [Tooltip("Grow multiplier")]
    public float growMultiplier = 1.05f;
    [Tooltip("Duration (seconds)")]
    public float growDuration = 8f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float growWeight = 60f;

    [Header("Power-Up: Shrink")]
    public bool shrinkEnabled = true;
    [Tooltip("Shrink multiplier")]
    public float shrinkMultiplier = 0.95f;
    [Tooltip("Duration (seconds)")]
    public float shrinkDuration = 8f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float shrinkWeight = 60f;

    [Header("Power-Up: Maze Move")]
    public bool mazeMoveEnabled = true;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float mazeMoveWeight = 50f;

    [Header("Power-Up: Phase Walk")]
    public bool phaseWalkEnabled = true;
    [Tooltip("Duration (seconds)")]
    public float phaseWalkDuration = 5f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float phaseWalkWeight = 40f;

    [Header("Power-Up: Player Wrap")]
    public bool playerWrapEnabled = true;
    [Tooltip("Duration (seconds)")]
    public float playerWrapDuration = 5f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float playerWrapWeight = 50f;

    [Header("Power-Up: Reverse KeyBinds")]
    public bool reverseKeyBindsEnabled = true;
    [Tooltip("Duration (seconds)")]
    public float reverseKeyBindsDuration = 5f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float reverseKeyBindsWeight = 55f;

    [Header("Power-Up: Clear Tails")]
    public bool clearTailsEnabled = true;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float clearTailsWeight = 30f;

    [Header("Power-Up: Camera Chaos")]
    public bool cameraChaosEnabled = true;
    [Tooltip("Duration (seconds)")]
    public float cameraChaosDuration = 5f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float cameraChaosWeight = 20f;

    [Header("Power-Up: Random Power-up")]
    public bool randomPowerUpEnabled = true;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float randomPowerUpWeight = 45f;

    [Header("Power-Up: Spawn Power-ups")]
    public bool spawnPowerUpsEnabled = true;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float spawnPowerUpsWeight = 25f;

    [Header("Power-Up: Wrapped Borders")]
    public bool wrappedBordersEnabled = true;
    [Tooltip("Duration (seconds)")]
    public float wrappedBordersDuration = 5f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float wrappedBordersWeight = 60f;

    public void ResetToDefaults()
    {
        playerSpeed = 1.5f;
        playerTurnSpeed = 180f;
        playerLineWidth = 0.2f;
        
        gapFrequency = 0.05f;
        gapSize = 15f;
        

        powerUpsEnabled = true;
        
        speedBoostEnabled = true;
        speedBoostMultiplier = 1.25f;
        speedBoostDuration = 6f;
        speedBoostWeight = 60f;
        
        slowDownEnabled = true;
        slowDownMultiplier = 0.75f;
        slowDownDuration = 6f;
        slowDownWeight = 60f;
        
        growEnabled = true;
        growMultiplier = 1.05f;
        growDuration = 8f;
        growWeight = 60f;
        
        shrinkMultiplier = 0.95f;
        shrinkDuration = 8f;
        shrinkWeight = 60f;

        mazeMoveEnabled = true;
        mazeMoveWeight = 50f;

        phaseWalkEnabled = true;
        phaseWalkDuration = 5f;
        phaseWalkWeight = 40f;

        playerWrapEnabled = true;
        playerWrapDuration = 8f;
        playerWrapWeight = 50f;

        reverseKeyBindsEnabled = true;
        reverseKeyBindsDuration = 5f;
        reverseKeyBindsWeight = 55f;

        clearTailsEnabled = true;
        clearTailsWeight = 30f;

        cameraChaosEnabled = true;
        cameraChaosDuration = 6f;
        cameraChaosWeight = 20f;

        randomPowerUpEnabled = true;
        randomPowerUpWeight = 45f;

        spawnPowerUpsEnabled = true;
        spawnPowerUpsWeight = 25f;

        wrappedBordersEnabled = true;
        wrappedBordersDuration = 10f;
        wrappedBordersWeight = 60f;
    }
}