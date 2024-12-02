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
        monsterCache = new MonsterCache();
    }
    #endregion

    private PoolManager poolManager;
    private EnemeyController monCon;

    [SerializeField]
    private MonsterData monData;
    private MonsterCache monsterCache;

    [Header("MonsterPool and SpawnPos")]
    [SerializeField]
    private ObjectPool monsterMainPool;
    [SerializeField]
    private GameObject[] SpawnPos;

    private GameObject targetMonster;
    private IRecivePoolObjects rpo;

    public void SpawnMonster(int MonIndex, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MonsterData.MonsterDataStructure data = monsterCache.GetData(MonIndex, monData.monsterDataList);
            if (data != null)
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
        MonsterData.MonsterDataStructure data = monsterCache.GetData(monIndex, monData.monsterDataList);
        if (data != null)
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
        else
        {
            Debug.LogError($"Monster data not found for ID: {monIndex}");
        }
    }

}
