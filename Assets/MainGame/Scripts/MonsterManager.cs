using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
          MonsterSpawner.mInst.SpawnMonster(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            MonsterSpawner.mInst.SpawnMonster(3, 1);
        }
    }
}