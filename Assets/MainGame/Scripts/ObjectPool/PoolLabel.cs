using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolLabel : MonoBehaviour
{
    protected ObjectPool mainPool;

    public virtual void Create(ObjectPool pool)
    {
        mainPool = pool;
        gameObject.SetActive(false);
    }

    public virtual void Push()
    {
        mainPool.Push(this);
    }
}
