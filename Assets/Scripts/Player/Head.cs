using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Head : MonoBehaviour
{
    public string Name;
    public int id;
    public Tail tail;
    public Color playerColor;

    [SerializeField] float baseSpeed = 1f;
    [SerializeField] float turnSpeed = 180f;
    [SerializeField] float blinkSpeed = 2f;

    public KeyCode LeftKey { get; private set; }
    public KeyCode RightKey { get; private set; }
    private KeyCode originalLeftKey, originalRightKey;

    private Border border;
    private SpriteRenderer spriteRenderer;
    private Coroutine warpBlinkRoutine;

    float input = 0;
    public bool isAlive = true;

    private Dictionary<string, int> effectCounts = new();

    public bool phaseWalk => GetEffect("phase") > 0;
    public bool mazeMove => GetEffect("maze") > 0;
    public bool playerWrap => GetEffect("wrap") > 0;

    public float sizeMultiplier { get; private set; } = 1f;
    private Vector3 baseScale;
    public float CurrentSize => sizeMultiplier;

    private List<float> speedModifiers = new();
    float currentSpeed => speedModifiers.Count == 0
        ? baseSpeed
        : baseSpeed * speedModifiers.Aggregate(1f, (acc, m) => acc * m);

    /* ========================
            UNITY LIFECYCLE
       ======================== */

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        border = GameObject.Find("GameBorder").GetComponent<Border>();
        baseScale = transform.localScale;

        var s = GameSettings.Instance;
        baseSpeed = s.playerSpeed;
        turnSpeed = s.playerTurnSpeed;
    }

    void Start() => GameManager.Instance.RegisterPlayer(this);

    void Update()
    {
        if (!isAlive) return;
        input = mazeMove ? 0 : GetInput();
        if (mazeMove) HandleMazeInput();
    }

    void FixedUpdate()
    {
        if (!isAlive) return;
        border.PlayerOffScreen(this);
        if (!mazeMove)
            transform.Rotate(Vector3.forward * turnSpeed * -input * Time.fixedDeltaTime, Space.Self);
        transform.Translate(Vector3.up * currentSpeed * Time.fixedDeltaTime, Space.Self);
    }

    /* ========================
              INPUT
       ======================== */

    float GetInput()
    {
        if (Input.GetKey(LeftKey)) return -1f;
        if (Input.GetKey(RightKey)) return 1f;
        return 0f;
    }

    void HandleMazeInput()
    {
        if (Input.GetKeyDown(LeftKey)) transform.Rotate(Vector3.forward * 90f);
        else if (Input.GetKeyDown(RightKey)) transform.Rotate(Vector3.forward * -90f);
    }

    /* ========================
            DEATH & COLLISION
       ======================== */

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive) return;
        if (collision.CompareTag("Head")) return;

        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp != null) { powerUp.Activate(this); Destroy(powerUp.gameObject); return; }

        if (phaseWalk && collision.CompareTag("Tail")) return;
        if ((border.wrappedActive || playerWrap) && collision.CompareTag("Border")) return;

        if (collision.CompareTag("Tail") && IsHittingOwnTailFromBehind(collision)) return;

        PlayerDeath();
    }

    bool IsHittingOwnTailFromBehind(Collider2D collision)
    {
        Tail hitTail = collision.GetComponentInParent<Tail>();
        if (hitTail == null || hitTail.Owner != this) return false;

        Vector2 toContact = ((Vector2)collision.ClosestPoint(transform.position)
                             - (Vector2)transform.position).normalized;
        return Vector2.Dot(transform.up, toContact) < 0f;
    }

    public void PlayerDeath(string customMessage = "")
    {
        isAlive = false;
        spriteRenderer.color = Color.gray;
        GameManager.Instance.CheckPlayers();
        ScoreHandler.Instance.OnPlayerDeath(id);
        Debug.Log(customMessage != "" ? customMessage : $"{Name} hit a wall!");
    }

    /* ========================
            SETUP
       ======================== */

    public void SetHead(string name, int newId, KeyCode leftKey, KeyCode rightKey, Color color)
    {
        Name = name; id = newId; playerColor = color;
        LeftKey = originalLeftKey = leftKey;
        RightKey = originalRightKey = rightKey;
    }

    /* ========================
            POWER-UPS
       ======================== */

    public IEnumerator ModifySpeed(float multiplier, float duration)
    {
        speedModifiers.Add(multiplier);
        yield return new WaitForSeconds(duration);
        speedModifiers.Remove(multiplier);
    }

    public IEnumerator ReverseKeyBinds(float duration)
    {
        if (AddEffect("reverse") == 1)
        {
            (LeftKey, RightKey) = (originalRightKey, originalLeftKey);
            spriteRenderer.color = Color.blue;
        }
        yield return new WaitForSeconds(duration);
        if (RemoveEffect("reverse") == 0)
        {
            (LeftKey, RightKey) = (originalLeftKey, originalRightKey);
            spriteRenderer.color = Color.yellow;
        }
    }

    public IEnumerator ActivatePhaseWalk(float duration)
    {
        AddEffect("phase");
        yield return new WaitForSeconds(duration);
        if (RemoveEffect("phase") == 0) tail.StartNewSegment();
    }

    public IEnumerator ActivateMazeMove(float duration)
    {
        AddEffect("maze");
        yield return new WaitForSeconds(duration);
        RemoveEffect("maze");
    }

    public IEnumerator ModifySize(float multiplier, float duration)
    {
        sizeMultiplier *= multiplier;
        ApplySize();
        AddEffect("size");
        yield return new WaitForSeconds(duration);
        sizeMultiplier /= multiplier;
        RemoveEffect("size");
        ApplySize();
    }

    public IEnumerator PlayerWarp(float duration)
    {
        if (AddEffect("wrap") == 1)
            warpBlinkRoutine = StartCoroutine(WarpBlink());
        yield return new WaitForSeconds(duration);
        if (RemoveEffect("wrap") == 0)
        {
            if (warpBlinkRoutine != null) StopCoroutine(warpBlinkRoutine);
        }
    }

    /* ========================
            HELPERS
       ======================== */

    void ApplySize()
    {
        transform.localScale = baseScale * sizeMultiplier;
        tail.StartNewSegment();
        tail.SetSize(sizeMultiplier);
    }

    int AddEffect(string key)
    {
        effectCounts.TryGetValue(key, out int count);
        effectCounts[key] = ++count;
        return count;
    }

    int RemoveEffect(string key)
    {
        effectCounts.TryGetValue(key, out int count);
        effectCounts[key] = --count;
        return count;
    }

    int GetEffect(string key)
    {
        effectCounts.TryGetValue(key, out int count);
        return count;
    }

    private IEnumerator WarpBlink()
    {
        while (true)
        {
            SetAlpha(Mathf.Lerp(0.2f, 1f, Mathf.PingPong(Time.time * blinkSpeed, 1f)));
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}