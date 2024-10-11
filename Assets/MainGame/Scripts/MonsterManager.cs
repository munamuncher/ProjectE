using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
          MonsterSpawner.mInst.SpawnMonster(0, 3);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            MonsterSpawner.mInst.SpawnMonster(1, 3);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            MonsterSpawner.mInst.SpawnMonster(2, 3);
        }
    }
}
