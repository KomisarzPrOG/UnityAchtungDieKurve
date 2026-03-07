using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    [Header("Referencja do ustawień")]
    [SerializeField] private GameSettings settings;

    [Header("Zakładki (TabHeader + TabContent)")]
    [SerializeField] private GameObject[] tabContents;
    [SerializeField] private Button[] tabHeaders;
    [SerializeField] private TextMeshProUGUI[] tabHeaderTexts;

    [Header("=== PLAYERS ===")]
    [SerializeField] private Slider sliderSpeed;
    [SerializeField] private TextMeshProUGUI labelSpeed;
    [SerializeField] private Slider sliderTurnSpeed;
    [SerializeField] private TextMeshProUGUI labelTurnSpeed;
    [SerializeField] private Slider sliderLineWidth;
    [SerializeField] private TextMeshProUGUI labelLineWidth;
    [SerializeField] private Slider sliderGapFrequency;
    [SerializeField] private TextMeshProUGUI labelGapFrequency;
    [SerializeField] private Slider sliderGapSize;
    [SerializeField] private TextMeshProUGUI labelGapSize;

    [Header("=== POWER-UPS (główny toggle) ===")]
    [SerializeField] private Toggle togglePowerUpsEnabled;

    [Header("Speed Boost")]
    [SerializeField] private Toggle toggleSpeedBoost;
    [SerializeField] private Slider sliderSpeedBoostMult;
    [SerializeField] private TextMeshProUGUI labelSpeedBoostMult;
    [SerializeField] private Slider sliderSpeedBoostDur;
    [SerializeField] private TextMeshProUGUI labelSpeedBoostDur;
    [SerializeField] private Slider sliderSpeedBoostWeig;
    [SerializeField] private TextMeshProUGUI labelSpeedBoostWeig;

    [Header("Slow Down")]
    [SerializeField] private Toggle toggleSlowDown;
    [SerializeField] private Slider sliderSlowDownMult;
    [SerializeField] private TextMeshProUGUI labelSlowDownMult;
    [SerializeField] private Slider sliderSlowDownDur;
    [SerializeField] private TextMeshProUGUI labelSlowDownDur;
    [SerializeField] private Slider sliderSlowDownWeig;
    [SerializeField] private TextMeshProUGUI labelSlowDownWeig;

    [Header("Grow")]
    [SerializeField] private Toggle toggleGrow;
    [SerializeField] private Slider sliderGrowMult;
    [SerializeField] private TextMeshProUGUI labelGrowMult;
    [SerializeField] private Slider sliderGrowDur;
    [SerializeField] private TextMeshProUGUI labelGrowDur;
    [SerializeField] private Slider sliderGrowWeig;
    [SerializeField] private TextMeshProUGUI labelGrowWeig;

    [Header("Shrink")]
    [SerializeField] private Toggle toggleShrink;
    [SerializeField] private Slider sliderShrinkMult;
    [SerializeField] private TextMeshProUGUI labelShrinkMult;
    [SerializeField] private Slider sliderShrinkDur;
    [SerializeField] private TextMeshProUGUI labelShrinkDur;
    [SerializeField] private Slider sliderShrinkWeig;
    [SerializeField] private TextMeshProUGUI labelShrinkWeig;

    [Header("Maze Move")]
    [SerializeField] private Toggle toggleMazeMove;
    [SerializeField] private Slider sliderMazeMoveWeig;
    [SerializeField] private TextMeshProUGUI labelMazeMoveWeig;

    [Header("Przyciski")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button resetButton;

    private bool[] _tabOpen;

    // -----------------------------------------------------------------------
    void Awake()
    {
        if (settings == null)
        {
            settings = Resources.Load<GameSettings>("GameSettings");
            Debug.LogWarning("GameSettings nie było podpięte w Inspektorze — załadowano przez Resources.Load");
        }

        _tabOpen = new bool[tabContents.Length];
    }

    void Start()
    {
        // Zamknij wszystkie zakładki na starcie
        for (int i = 0; i < tabContents.Length; i++)
        {
            tabContents[i].SetActive(false);
            _tabOpen[i] = false;
            int idx = i; // capture for lambda
            tabHeaders[i].onClick.AddListener(() => ToggleTab(idx));
        }

        LoadSettingsToUI();

        backButton.onClick.AddListener(OnBack);
        if (resetButton != null)
            resetButton.onClick.AddListener(OnReset);

        RegisterSliderListeners();
        RegisterToggleListeners();
    }

    // -----------------------------------------------------------------------
    // Accordion — toggleuje zakładkę
    public void ToggleTab(int index)
    {
        _tabOpen[index] = !_tabOpen[index];
        tabContents[index].SetActive(_tabOpen[index]);
        // Obróć strzałkę w nagłówku
        if (tabHeaderTexts != null && index < tabHeaderTexts.Length)
        {
            string name = tabHeaderTexts[index].text;
            // Zamień ▶ ↔ ▼
            if (name.StartsWith("▶")) name = "▼" + name.Substring(1);
            else if (name.StartsWith("▼")) name = "▶" + name.Substring(1);
            tabHeaderTexts[index].text = name;
        }
    }

    // -----------------------------------------------------------------------
    void LoadSettingsToUI()
    {
        // Players
        SetSlider(sliderSpeed, labelSpeed, settings.playerSpeed, 1f, 20f, "Prędkość: {0:F1}");
        SetSlider(sliderTurnSpeed, labelTurnSpeed, settings.playerTurnSpeed, 30f, 360f, "Skręt: {0:F0}°/s");
        SetSlider(sliderLineWidth, labelLineWidth, settings.playerLineWidth, 0.05f, 0.5f, "Grubość: {0:F2}");
        SetSlider(sliderGapFrequency, labelGapFrequency, settings.gapFrequency, 0.01f, 0.3f, "Dziury (częst.): {0:F2}");
        SetSlider(sliderGapSize, labelGapSize, settings.gapSize, 0.5f, 30f, "Rozmiar dziury: {0:F1}");

        // Power-ups
        SetToggle(togglePowerUpsEnabled, settings.powerUpsEnabled);

        SetToggle(toggleSpeedBoost, settings.speedBoostEnabled);
        SetSlider(sliderSpeedBoostMult, labelSpeedBoostMult, settings.speedBoostMultiplier, 1.1f, 5f, "Mnożnik: {0:F2}x");
        SetSlider(sliderSpeedBoostDur, labelSpeedBoostDur, settings.speedBoostDuration, 1f, 15f, "Czas: {0:F0}s");
        SetSlider(sliderSpeedBoostWeig, labelSpeedBoostWeig, settings.speedBoostWeight, 0f, 100f, "Szansa: {0:F0}%");

        SetToggle(toggleSlowDown, settings.slowDownEnabled);
        SetSlider(sliderSlowDownMult, labelSlowDownMult, settings.slowDownMultiplier, 0.1f, 0.95f, "Mnożnik: {0:F2}x");
        SetSlider(sliderSlowDownDur, labelSlowDownDur, settings.slowDownDuration, 1f, 15f, "Czas: {0:F0}s");
        SetSlider(sliderSlowDownWeig, labelSlowDownWeig, settings.slowDownWeight, 0f, 100f, "Szansa: {0:F0}%");

        SetToggle(toggleGrow, settings.growEnabled);
        SetSlider(sliderGrowMult, labelGrowMult, settings.growMultiplier, 1.15f, 6f, "Mnożnik: {0:F2}x");
        SetSlider(sliderGrowDur, labelGrowDur, settings.growDuration, 1f, 15f, "Czas: {0:F0}s");
        SetSlider(sliderGrowWeig, labelGrowWeig, settings.growWeight, 0f, 100f, "Szansa: {0:F0}%");

        SetToggle(toggleShrink, settings.shrinkEnabled);
        SetSlider(sliderShrinkMult, labelShrinkMult, settings.shrinkMultiplier, 0.5f, 0.99f, "Mnożnik: {0:F2}x");
        SetSlider(sliderShrinkDur, labelShrinkDur, settings.shrinkDuration, 1f, 15f, "Czas: {0:F0}s");
        SetSlider(sliderShrinkWeig, labelShrinkWeig, settings.shrinkWeight, 0f, 100f, "Szansa: {0:F0}%");

        SetToggle(toggleMazeMove, settings.mazeMoveEnabled);
        SetSlider(sliderMazeMoveWeig, labelMazeMoveWeig, settings.mazeMoveWeight, 0f, 100f, "Szansa: {0:F0}%");
    }

    void RegisterSliderListeners()
    {
        // Players
        AddSliderListener(sliderSpeed, labelSpeed, "Prędkość: {0:F1}", v => settings.playerSpeed = v);
        AddSliderListener(sliderTurnSpeed, labelTurnSpeed, "Skręt: {0:F0}°/s", v => settings.playerTurnSpeed = v);
        AddSliderListener(sliderLineWidth, labelLineWidth, "Grubość: {0:F2}", v => settings.playerLineWidth = v);
        AddSliderListener(sliderGapFrequency, labelGapFrequency, "Dziury (częst.): {0:F2}", v => settings.gapFrequency = v);
        AddSliderListener(sliderGapSize, labelGapSize, "Rozmiar dziury: {0:F1}", v => settings.gapSize = v);

        // Speed Boost
        AddSliderListener(sliderSpeedBoostMult, labelSpeedBoostMult, "Mnożnik: {0:F2}x", v => settings.speedBoostMultiplier = v);
        AddSliderListener(sliderSpeedBoostDur, labelSpeedBoostDur, "Czas: {0:F0}s", v => settings.speedBoostDuration = v);
        AddSliderListener(sliderSpeedBoostWeig, labelSpeedBoostWeig, "Szansa: {0:F0}%", v => settings.speedBoostWeight = v);

        // Slow Down
        AddSliderListener(sliderSlowDownMult, labelSlowDownMult, "Mnożnik: {0:F2}x", v => settings.slowDownMultiplier = v);
        AddSliderListener(sliderSlowDownDur, labelSlowDownDur, "Czas: {0:F0}s", v => settings.slowDownDuration = v);
        AddSliderListener(sliderSlowDownWeig, labelSlowDownWeig, "Szansa: {0:F0}%", v => settings.slowDownWeight = v);

        // Grow
        AddSliderListener(sliderGrowMult, labelGrowMult, "Mnożnik: {0:F2}x", v => settings.growMultiplier = v);
        AddSliderListener(sliderGrowDur, labelGrowDur, "Czas: {0:F0}s", v => settings.growDuration = v);
        AddSliderListener(sliderGrowWeig, labelGrowWeig, "Szansa: {0:F0}%", v => settings.growWeight= v);

        // Shrink
        AddSliderListener(sliderShrinkMult, labelShrinkMult, "Mnożnik: {0:F2}x", v => settings.shrinkMultiplier = v);
        AddSliderListener(sliderShrinkDur, labelShrinkDur, "Czas: {0:F0}s", v => settings.shrinkDuration = v);
        AddSliderListener(sliderShrinkWeig, labelShrinkWeig, "Szansa: {0:F0}%", v => settings.shrinkWeight = v);

        // Maze Move
        AddSliderListener(sliderMazeMoveWeig, labelMazeMoveWeig, "Szansa: {0:F0}%", v => settings.mazeMoveWeight = v);
    }

    void RegisterToggleListeners()
    {
        AddToggleListener(togglePowerUpsEnabled, v => settings.powerUpsEnabled = v);
        AddToggleListener(toggleSpeedBoost, v => settings.speedBoostEnabled = v);
        AddToggleListener(toggleSlowDown, v => settings.slowDownEnabled = v);
        AddToggleListener(toggleGrow, v => settings.growEnabled = v);
        AddToggleListener(toggleShrink, v => settings.shrinkEnabled = v);
        AddToggleListener(toggleMazeMove, v => settings.mazeMoveEnabled = v);
    }

    // -----------------------------------------------------------------------
    void OnBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnReset()
    {
        settings.ResetToDefaults();
        LoadSettingsToUI();
    }

    // -----------------------------------------------------------------------
    // Helpery
    void SetSlider(Slider s, TextMeshProUGUI label, float value, float min, float max, string fmt)
    {
        if (s == null) return;
        s.minValue = min;
        s.maxValue = max;
        s.value = value;
        if (label != null) label.text = string.Format(fmt, value);
    }

    void SetToggle(Toggle t, bool value)
    {
        if (t == null) return;
        t.isOn = value;
    }

    void AddSliderListener(Slider s, TextMeshProUGUI label, string fmt, System.Action<float> setter)
    {
        if (s == null) return;
        s.onValueChanged.AddListener(v =>
        {
            setter(v);
            if (label != null) label.text = string.Format(fmt, v);
        });
    }

    void AddToggleListener(Toggle t, System.Action<bool> setter)
    {
        if (t == null) return;
        t.onValueChanged.AddListener(v => setter(v));
    }
}