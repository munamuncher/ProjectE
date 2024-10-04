using System.Collections;
using System.Collections.Generic;
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
    }
    #endregion

    private PoolManager poolManager;
    private EnemeyController monCon;
    [SerializeField]
    private GameObject[] SpawnPos;

    [SerializeField]
    private MonsterData monData;
    private GameObject targetMonster;

    public void SpawnMonster(int MonIndex, int amount)
    {
        poolManager = PoolManager.pinst;   
        for(int i = 0; i < amount; i++)
        {
            targetMonster = poolManager.pools[1].Pop();
            int ran = Random.Range(0, SpawnPos.Length);
            targetMonster.transform.position = SpawnPos[ran].transform.position;

            MonsterData.MonsterDataStructure data = RetrieveMonsterData(MonIndex);
            if(data != null)
            {
                if(!targetMonster.TryGetComponent<EnemeyController>(out monCon))
                {
                    Debug.LogError("풀에서 생성된 몬스터에서 EnemyController.cs 참조 실패");
                }
                monCon.ReciveStatus(data.health, data.Armor, data.damage);
            }
        }
    }


    private MonsterData.MonsterDataStructure RetrieveMonsterData(int index)
    {
        foreach (var data in monData.monsterDataList)
        {
            if (data.id == index)
            {
                return data;
            }
        }
        return null;
    }
}
