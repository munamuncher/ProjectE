using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class labelUsage : PoolLabel
{
    private float DeathTimer = 3f;

    private void OnEnable()
    {
        DeathTimer = 3f;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 10f);
        DeathTimer -= Time.deltaTime;
        if(DeathTimer<=0)
        {
            Push();
        }
        
    }

}
