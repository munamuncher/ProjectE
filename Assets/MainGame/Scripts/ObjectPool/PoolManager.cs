using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
   private static PoolManager instance;
   public static PoolManager pinst => instance;

    private void Awake()
    {
        if(instance != this && instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public List<ObjectPool> pools = new List<ObjectPool>();

}
