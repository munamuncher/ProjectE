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

    private int currentMana = 1000;
    
    private void Start()
    {
        skillList = new List<Skill>
        {
            new FireBall(),
            new Thunder(),
            new ShockWave()
        };

      
        currentSkill = skillList[0];
    }

    private void Update()
    {
        foreach(var skill in skillList)
        {
            skill.UpdateCooldown();
        }
    }

    public void CastSpell(int index)
    {
        if (index >= 0 && index < skillList.Count)
        {
            RetiveData(index);
            currentSkill = skillList[index];
            if(currentSkill.HasEnoughMana(currentMana) && !currentSkill.IsOnCooldown())
            {
                currentSkill.UseMana(currentMana);
                Debug.Log($"Casting skill at index {index}, which is {currentSkill.GetType().Name}");
                currentSkill.UseSkill();
            }
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
                Debug.Log($"Retrieved data for {skill.GetType().Name}: Mana = {skill.manaCost}, Cooldown = {skill.coolDownTime}");
            }
        }
        
    }
}
