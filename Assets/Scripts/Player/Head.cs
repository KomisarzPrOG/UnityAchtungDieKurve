using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public string Name;
    public int id;
    [SerializeField] public Tail tail;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float movmentSpeed = 1f;
    [SerializeField] float turnSpeed = 180f;

    public KeyCode LeftKey;
    public KeyCode RightKey;

    float input = 0;

    public bool isAlive = true;
    public Color playerColor { get; private set; }

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
        transform.Translate(Vector3.up * movmentSpeed * Time.fixedDeltaTime, Space.Self);
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
}
