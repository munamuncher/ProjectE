using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCache : DataCache<SkillData.SkillDataStructure>
{
    protected override int GetId(SkillData.SkillDataStructure data)
    {
        return data.id;
    }
}
