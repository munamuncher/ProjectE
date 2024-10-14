using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    #region _singleton_
    private static MonsterSpawner moninst;

    public static MonsterSpawner mInst => moninst;

    private void Awake()
    {
        if (moninst != this && moninst)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            moninst = this;
            DontDestroyOnLoad(gameObject);
        }

        CacheMonsterData();
    }
    #endregion

    private PoolManager poolManager;
    private EnemeyController monCon;
    [SerializeField]
    private GameObject[] SpawnPos;
    [SerializeField]
    private MonsterData monData;
    private GameObject targetMonster;

    private Dictionary<int, MonsterData.MonsterDataStructure> monDataDictCache = new Dictionary<int, MonsterData.MonsterDataStructure>();

    private void CacheMonsterData()
    {
        foreach(var data in monData.monsterDataList)
        {
            if(!monDataDictCache.ContainsKey(data.id))
            {
                monDataDictCache.Add(data.id, data);
            }
        }
    }


    public void SpawnMonster(int MonIndex, int amount)
    {
        poolManager = PoolManager.pinst;   
        for(int i = 0; i < amount; i++)
        {
            targetMonster = poolManager.pools[1].Pop();
            int ran = Random.Range(0, SpawnPos.Length);
            targetMonster.transform.position = SpawnPos[ran].transform.position;
            if (monDataDictCache.TryGetValue(MonIndex, out MonsterData.MonsterDataStructure data))
            {
                if (!targetMonster.TryGetComponent<EnemeyController>(out monCon))
                {
                    Debug.LogError("풀에서 생성된 몬스터에서 EnemyController.cs 참조 실패");
                }
                if (data.MonsterAnim != null)
                {
                    monCon.ReciveStatus(data.health, data.Armor, data.damage, data.MonsterAnim);
                }
                else
                {
                    Debug.LogError($"{data.MonsterAnim} = null");
                }
            }
            else
            {
                Debug.LogError($"Monster data not found for ID: {MonIndex}");
            }
        }
    }
    //private MonsterData.MonsterDataStructure RetrieveMonsterData(int index)
    //{
    //    foreach (var data in monData.monsterDataList)
    //    {
    //        if (data.id == index)
    //        {               
    //            return data;
    //        }
    //    }
    //    return null;
    //}

}
