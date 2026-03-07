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
    [Tooltip("Duration (sekundy)")]
    public float shrinkDuration = 8f;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float shrinkWeight = 60f;

    [Header("Power-Up: Maze Move")]
    public bool mazeMoveEnabled = true;
    [Tooltip("Chance to spawn this power-up")]
    [Range(0, 100f)]
    public float mazeMoveWeight = 50f;

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
    }
}