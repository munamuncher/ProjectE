using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//todo list
//spell with buttons
//will weapons and equiptment effect the dmg? or just add another damage and multiply it
//how many skill and jobs??? so far warrior and mage so far

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
        spellCache = new SpellCache();
        skillDamageCalc = new SkillDamageCalc();
    }
    #endregion

    [SerializeField]
    private SkillData skillData;
    [SerializeField]
    private List<Skill> skillList;
    private Skill currentSkill;

    private PoolManager poolManager;
    public GameObject castingPoint;
    private SpellCache spellCache;

    private IChangeSkill changeSkill;
    private ISkillMove skillMove;
    private ISkillDamageCalc skillDamageCalc;

    private int currentMana = 1000;
    [SerializeField]
    private int baseDamage = 0;
    [SerializeField]
    private int charBonusDmg = 0;
    [SerializeField]
    private int finalDamage = 0;
    private void Start()
    {
        skillList = new List<Skill> { new FireBall(), new Thunder(), new ShockWave() };
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CastSpell(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CastSpell(1);
        }

        foreach (var skill in skillList)
        {
            skill.UpdateCooldown();
        }
    }

    public void ReciveCharacterMagic(int characterBonus)
    {
        charBonusDmg = characterBonus;
    }

    public void Initialize(ISkillDamageCalc calculator, int characterBonus)
    {
        skillDamageCalc = calculator;
        finalDamage = skillDamageCalc.CalculateSkillDmg(baseDamage, characterBonus);
    }

    public void CastSpell(int index)
    {
        poolManager = PoolManager.pinst;
        if (index >= 0 && index < skillList.Count)
        {
            currentSkill = skillList[index];
            if (currentSkill.HasEnoughMana(currentMana) && !currentSkill.IsOnCooldown())
            {
                GameObject spell = poolManager.pools[0].Pop();
                if (!spell.TryGetComponent<IChangeSkill>(out changeSkill))
                {
                    Debug.LogError("IChangeSkill component not found on the spell prefab.");
                    return;
                }
                if (!spell.TryGetComponent<ISkillMove>(out skillMove))
                {
                    Debug.LogError("ISkillMove component not found on the spell prefab.");
                    return;
                }
                SendDirection(spell);
                var data = spellCache.GetData(index, skillData.skillDataList);
                if (data != null)
                {
                    ApplySkillData(data); 
                }
                else
                {
                    Debug.LogWarning($"Skill data for ID: {index} not found.");
                }
                spell.transform.position = castingPoint.transform.position;

                currentSkill.UseMana(currentMana);
                Debug.Log($"Casting skill at index {index}, which is {currentSkill.GetType().Name}");
                changeSkill.ReciveDamageData(finalDamage);
                Initialize(skillDamageCalc, charBonusDmg);
                currentSkill.UseSkill();
                currentSkill.UpdateCooldown();
            }
            else
            {
                Debug.Log("Skill is on cooldown or not enough mana.");
            }
        }
        else
        {
            Debug.LogError("Skill index out of range.");
        }
    }

    private void SendDirection(GameObject spell)
    {
        int direction = transform.parent.localScale.x == 1 ? 1 : -1;
        skillMove.ReciveDirection(direction);
        spell.transform.localScale = new Vector3(direction * 5, 5, 5);
        Debug.Log($"Player is looking {(direction == 1 ? "Right" : "Left")}");
    }

    private void ApplySkillData(SkillData.SkillDataStructure data)
    {
        Skill skill = skillList[data.id];
        skill.manaCost = data.mana;
        skill.coolDownTime = data.coolDownTime;
        Debug.Log($"Retrieved data for {skill.GetType().Name}: Mana = {skill.manaCost}, Cooldown = {skill.coolDownTime}, SpellSprite = {data.spellAnimation.name}");
        changeSkill.ReciveSprite(data.spellAnimation);
        baseDamage = data.damage;
        Debug.LogWarning($"baseDamage is :{baseDamage}");
        Debug.Log($"Skill sprite has changed to {data.spellAnimation.name}");
    }
}
