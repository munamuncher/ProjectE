using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataStructureList", menuName = "Skill", order = 1)]
[System.Serializable]
public class SkillData : ScriptableObject
{

    public List<SkillDataStructure> skillDataList = new List<SkillDataStructure>();

    public Dictionary<int, SkillDataStructure> skillDataDictionary;

    private void OnEnable()
    {
        InitializeSkillDictionary();
    }

    private void InitializeSkillDictionary()
    {
        skillDataDictionary = new Dictionary<int, SkillDataStructure>();

        foreach (var skill in skillDataList)
        {
            if (skill == null)
            {
                Debug.LogWarning("Found a null skill in skillDataList. Skipping...");
                continue;
            }

            if (!skillDataDictionary.ContainsKey(skill.id))
            {
                skillDataDictionary.Add(skill.id, skill);
            }
            else
            {
                Debug.LogError($"Duplicate skill ID found: {skill.skillName}. Skipping this skill.");
            }
        }
    }

    [System.Serializable]
    public class SkillDataStructure
    {
        public int id;
        public string skillName;
        public int mana;
        public float coolDownTime;
        public string skillDescription;
    }

    public SkillDataStructure GetSkill(int id)
    {
        if (skillDataDictionary.TryGetValue(id, out var skill))
        {
            return skill;
        }
        Debug.LogWarning($"Skill not found: {id}");
        return null;
    }
}