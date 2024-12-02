using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataCache <T>
{
    protected Dictionary<int , T> cache = new Dictionary<int, T> ();
    public T GetData(int id, List<T> dataList)
    {
        if(!cache.TryGetValue(id, out T data))
        {
            data = dataList.Find(d => GetId(d) ==  id);
            if(data !=  null)
            {
                cache[id] = data;
                Debug.Log($"Data for ID: {id} added to cache.");
            }
            else
            {
                Debug.LogWarning($"Data not found for ID:{id} .");
            }
        }
        else
        {
            Debug.Log($"Data for ID: {id} retrieved from cache"); 
        }
        return data;
    }

    protected abstract int GetId(T data);
}
