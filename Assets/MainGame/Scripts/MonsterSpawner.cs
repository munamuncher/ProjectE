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
        poolManager = PoolManager.pinst;
        CacheMonsterData();
    }
    #endregion

    private PoolManager poolManager;
    private EnemeyController monCon;
    [SerializeField]
    private GameObject[] SpawnPos;
    [SerializeField]
    private MonsterData monData;
    [SerializeField]
    private ObjectPool monsterMainPool;

    private GameObject targetMonster;
    private IRecivePoolObjects rpo;

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
     
        for (int i = 0; i < amount; i++)
        {
            if (monDataDictCache.TryGetValue(MonIndex, out MonsterData.MonsterDataStructure data))
            {

                targetMonster = poolManager.pools[1].Pop();
                int ran = Random.Range(0, SpawnPos.Length);
                targetMonster.transform.position = SpawnPos[ran].transform.position;   
                Debug.Log($"Spawning Monster {targetMonster.name}");
                if (!targetMonster.TryGetComponent<EnemeyController>(out monCon))
                {
                    Debug.LogError("풀에서 생성된 몬스터에서 EnemyController.cs 참조 실패");
                }
                else
                {
                    monCon.ReciveStatus(data.health, data.Armor, data.damage);
                }
            }
            else
            {
                Debug.LogError($"Monster data not found for ID: {MonIndex}");
            }

        }
    }

    public void ReciveMonsterGameObject(int monIndex)
    {
        if(monDataDictCache.TryGetValue(monIndex, out MonsterData.MonsterDataStructure data))
        {

            if (!monsterMainPool.TryGetComponent<IRecivePoolObjects>(out rpo))
            {
                Debug.LogError("IRecivePoolObject 참조 실패 - MonsterSpawner.cs - spawnMonster");
            }
            else
            {
                monsterMainPool.ReciveGameObject(data.MonsterPrefab);       
                Debug.Log($"Recived {monIndex} so Spawning {data.MonsterPrefab}");
            }
        }
    }
    
}
