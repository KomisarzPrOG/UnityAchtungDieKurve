using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public string Name;
    public int id;
    public Tail tail;
    [SerializeField] float baseSpeed = 1f;
    [SerializeField] float turnSpeed = 180f;

    public KeyCode LeftKey;
    public KeyCode RightKey;

    private int reverseEffectCount = 0;
    private KeyCode originalLeftKey;
    private KeyCode originalRightKey;

    private Border border;

    float input = 0;

    public bool isAlive = true;
    public Color playerColor;

    private List<float> speedModifiers = new List<float>();
    float currentSpeed
    {
        get
        {
            float speed = baseSpeed;

            foreach (float modifier in speedModifiers)
                speed *= modifier;

            return speed;
        }
    }

    [SerializeField] float blinkSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private Coroutine blinkRoutine;

    public bool phaseWalk { get; private set; } = false;
    int phaseWalkCount = 0;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        border = GameObject.Find("GameBorder").GetComponent<Border>();
    }

    void Start()
    {
        GameManager.Instance.RegisterPlayer(this);
    }

    void Update()
    {
        if (!isAlive) return;
            
        input = getInput();
    }

    void FixedUpdate()
    {
        if(!isAlive) return;

        border.PlayerOffScreen(this);

        transform.Rotate(Vector3.forward * turnSpeed * -input * Time.fixedDeltaTime, Space.Self);
        transform.Translate(Vector3.up * currentSpeed * Time.fixedDeltaTime, Space.Self);
    }

    float getInput()
    {
        if (Input.GetKey(LeftKey)) return -1f;
        else if(Input.GetKey(RightKey)) return 1f;
        return 0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive) return;

        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp != null)
        {
            powerUp.Activate(this);
            Destroy(powerUp.gameObject);
            return;
        }

        if (phaseWalk && collision.CompareTag("Tail"))
            return;

        if(border.wrappedActive && collision.CompareTag("Border"))
            return;

        PlayerDeath();
    }

    public void PlayerDeath(string customMessage = "")
    {
        isAlive = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;

        GameManager.Instance.CheckPlayers();
        ScoreHandler.Instance.OnPlayerDeath(id);
        
        Debug.Log(customMessage != "" ? customMessage : $"{Name} hit a wall!");
    }

    public void SetHead(string name, int newId, KeyCode leftKey, KeyCode rightKey, Color color)
    {
        Name = name;
        id = newId;
        LeftKey = leftKey;
        RightKey = rightKey;
        playerColor = color;

        originalLeftKey = leftKey;
        originalRightKey = rightKey;
    }

    /* ====================
          POWERUP METHODS
       ==================== */

    public IEnumerator ModifySpeed(float multiplier, float duration)
    {
        speedModifiers.Add(multiplier);
        yield return new WaitForSeconds(duration);
        speedModifiers.Remove(multiplier);
    }

    public IEnumerator ReverseKeyBinds(float duration)
    {
        reverseEffectCount++;

        if(reverseEffectCount == 1)
        {
            LeftKey = originalRightKey;
            RightKey = originalLeftKey;
            spriteRenderer.color = Color.blue;
        }

        yield return new WaitForSeconds(duration);

        reverseEffectCount--;

        if(reverseEffectCount == 0)
        {
            LeftKey = originalLeftKey;
            RightKey = originalRightKey;
            spriteRenderer.color = Color.yellow;
        }
    }

    public IEnumerator ActivatePhaseWalk(float duration)
    {
        phaseWalkCount++;

        if(phaseWalkCount == 1)
            phaseWalk = true;

        yield return new WaitForSeconds(duration);

        phaseWalkCount--;

        if(phaseWalkCount == 0)
        {
            tail.StartNewSegment();

            phaseWalk = false;
        }
    }

    private IEnumerator PhaseBlink()
    {
        while (true)
        {
            float alpha = Mathf.Lerp(0.2f, 1f,
                Mathf.PingPong(Time.time * blinkSpeed, 1f));

            SetAlpha(alpha);

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
