using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolLabel : MonoBehaviour
{
    [SerializeField]
    protected ObjectPool mainPool;

    public virtual void Create(ObjectPool pool)
    {
        mainPool = pool;
        gameObject.SetActive(false);
    }

    public virtual void Push()
    {
        if(mainPool == null)
        {
            Debug.Log("Mainpool is null");
        }
        mainPool.Push(this);
    }
}
