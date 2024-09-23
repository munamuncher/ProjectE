using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    #region _singleton_
    private static SkillManager skillinst;

    public static SkillManager sInst => skillinst;



    private void Awake()
    {
        if (skillinst != this && skillinst)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            skillinst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [SerializeField]
    private SkillData skillData;
    [SerializeField]
    private List<Skill> skillList;
    private Skill currentSkill;
    private void Start()
    {
        skillList = new List<Skill>
        {
            new FireBall(),
            new Thunder()
        };

        currentSkill = skillList[1];
        currentSkill.mana = 100;
    }

    private void Update()
    {
        foreach(var skill in skillList)
        {
            skill.UpdateCooldown();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            CastSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            CastSpell(1);
        }
    }

    private void CastSpell(int index)
    {
        if(index >= 0 && index <skillList.Count)
        {
            RetiveData(index);
            currentSkill = skillList[index];
            Debug.Log($"castin spell {index} spell number");
            Debug.Log($"currect Skill is  {currentSkill}");
            currentSkill.UseSkill();
        }
        else
        {
            Debug.LogError("Skill index out of range");
        }             
    }

    private void RetiveData(int index)
    {
        Debug.Log("retriving data");
        foreach(var data in skillData.skillDataList)
        {
            if (data.id == index)
            {
                Skill skill = skillList[index];
                skill.manaCost = data.mana;
                skill.coolDownTime = data.coolDownTime;                
                Debug.Log("the mana cost for this is = "+skill.manaCost);
                Debug.Log("the cooldown for the spell is this = "+skill.coolDownTime);
                Debug.Log("the skill name is this = "+data.skillName);
            }
        }
    }
}
