using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public string Name;
    public int id;
    [SerializeField] public Tail tail;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float baseSpeed = 1f;
    [SerializeField] float turnSpeed = 180f;

    public KeyCode LeftKey;
    public KeyCode RightKey;

    float input = 0;

    public bool isAlive = true;
    public Color playerColor { get; private set; }

    private List<float> speedModifiers = new List<float>();
    float currentSpeed
    {
        get
        {
            float speed = baseSpeed;

            foreach(float modifier in speedModifiers)
                speed *= modifier;

            return speed;
        }
    }

    void Start()
    {
        playerColor = lineRenderer.material.color;
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

        isAlive = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;

        GameManager.Instance.CheckPlayers();
        ScoreHandler.Instance.OnPlayerDeath(id);
        
        Debug.Log($"{Name} uderzy³ w œciane!");
    }

    public void SetHead(string name, int newId, KeyCode leftKey, KeyCode rightKey, Color color)
    {
        Name = name;
        id = newId;
        LeftKey = leftKey;
        RightKey = rightKey;
        playerColor = color;
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
}
