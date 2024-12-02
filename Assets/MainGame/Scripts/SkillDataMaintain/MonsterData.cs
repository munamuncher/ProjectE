using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterDataStucture", menuName = "Monster", order = 1)]
[System.Serializable]
public class MonsterData : ScriptableObject
{
    public List<MonsterDataStructure> monsterDataList = new List<MonsterDataStructure>();

    public Dictionary<int, MonsterDataStructure> MonsterDataDictionary;

    private void OnEnable()
    {
        InitializeSkillDictionary();
    }

    private void InitializeSkillDictionary()
    {
        MonsterDataDictionary = new Dictionary<int, MonsterDataStructure>();

        foreach (var monster in monsterDataList)
        {
            if (monster == null)
            {
                Debug.LogWarning("Found a null skill in skillDataList. Skipping...");
                continue;
            }

            if (!MonsterDataDictionary.ContainsKey(monster.id))
            {
                MonsterDataDictionary.Add(monster.id, monster);
            }
            else
            {
                Debug.LogError($"Duplicate skill ID found: {monster.monsterName}. Skipping this skill.");
            }
        }
    }

    [System.Serializable]
    public class MonsterDataStructure
    {
        public int id;
        public string monsterName;
        public int health;
        public int Armor;
        public int damage;
        public GameObject MonsterPrefab;
    }

    public MonsterDataStructure GetSkill(int id)
    {
        if (MonsterDataDictionary.TryGetValue(id, out var monster))
        {
            return monster;
        }
        Debug.LogWarning($"Skill not found: {id}");
        return null;
    }
}
