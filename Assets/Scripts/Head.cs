using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public string Name;
    [SerializeField] float movmentSpeed = 1f;
    [SerializeField] float turnSpeed = 180f;

    public KeyCode LeftKey;
    public KeyCode RightKey;

    float input = 0;

    public bool isAlive { get; private set; } = true;

    void Start()
    {
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
        
        Debug.Log($"{Name} uderzy³ w œciane!");
    }
}
