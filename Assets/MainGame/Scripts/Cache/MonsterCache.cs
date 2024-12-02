using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCache : DataCache<MonsterData.MonsterDataStructure>
{
    protected override int GetId(MonsterData.MonsterDataStructure data)
    {
        return data.id;
    }
}
