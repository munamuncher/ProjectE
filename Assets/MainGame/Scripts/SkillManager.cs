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

    private PoolManager poolManager;
    public GameObject castingPoint;

    [SerializeField]
    private GameObject attackSpell;
    private IChangeSkill changeSkill;

    private int currentMana = 1000;
    
    private void Start()
    {
        skillList = new List<Skill>
        {
            new FireBall(),
            new Thunder(),
            new ShockWave()
        }; 
        if(attackSpell != null)
        {
            if(!attackSpell.TryGetComponent<IChangeSkill>(out changeSkill))
            {
                Debug.LogError("iChangeSkill 참조 실패 - SkillManager.cs - Awake()");
            }
        }
        else
        {
            Debug.LogError("atttackSpell null - SkillManager.cs - Awake()");
        }
        currentSkill = skillList[0];
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.A))
        {
            CastSpell(0);
        }

        foreach(var skill in skillList)
        {
            skill.UpdateCooldown();
        }
    }

    public void CastSpell(int index)
    {
        poolManager = PoolManager.pinst;
        if (index >= 0 && index < skillList.Count)
        {
            RetriveData(index);
            currentSkill = skillList[index];
            GameObject spell = poolManager.pools[index].Pop();
            spell.transform.position = castingPoint.transform.position;
            if (currentSkill.HasEnoughMana(currentMana) && !currentSkill.IsOnCooldown())
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

    private void RetriveData(int index)
    {
        Debug.Log("retriving data");
        foreach(var data in skillData.skillDataList)
        {
            if (data.id == index)
            {
                Skill skill = skillList[index];
                skill.manaCost = data.mana;
                skill.coolDownTime = data.coolDownTime;
                Debug.Log($"Retrieved data for {skill.GetType().Name}: Mana = {skill.manaCost}, Cooldown = {skill.coolDownTime}, SpellSprite = {data.spellSprite.name}");
                changeSkill.ReciveSprite(data.spellSprite);
                Debug.Log($"skill sprite has changes to {data.spellSprite.name}");
            }
        }
        
    }
}
