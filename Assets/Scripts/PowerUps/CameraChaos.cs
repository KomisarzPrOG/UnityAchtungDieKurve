using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaos : MonoBehaviour
{
    private float chaosTimer = 0;
    private float chaosDuration = 0;

    private float originalZ;
    private bool isActive = false;

    void Awake()
    {
        originalZ = transform.eulerAngles.z;    
    }

    void Update()
    {
        if (!isActive) return;

        chaosTimer += Time.deltaTime;

        float t = chaosTimer / chaosDuration;
        float angle = 360f * t;

        transform.rotation = Quaternion.Euler(0,0,angle);

        if(chaosTimer >= chaosDuration)
        {
            isActive = false;
            transform.rotation = Quaternion.Euler(0, 0, originalZ);
            chaosTimer = 0;
        }
    }

    public void ActivateChaos(float duration)
    {
        if(!isActive)
        {
            isActive = true;
            chaosDuration = duration;
            chaosTimer = 0;
        }
        else
        {
            chaosDuration += duration;
        }
    }
}
