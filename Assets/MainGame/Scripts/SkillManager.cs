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
    private bool isLeftorRight = true;
    private Skill currentSkill;

    private PoolManager poolManager;
    public GameObject castingPoint;
    private IChangeSkill changeSkill;
    private ISkillMove skillMove;

    private int currentMana = 1000;
    
    private void Start()
    {
        skillList = new List<Skill>
        {
            new FireBall(),
            new Thunder(),
            new ShockWave()
        }; 
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.A))
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
  
    public void CastSpell(int index)
    {
        poolManager = PoolManager.pinst;
        if (index >= 0 && index < skillList.Count)
        {
            
            currentSkill = skillList[index];
            GameObject spell = poolManager.pools[0].Pop();            
            if(!spell.TryGetComponent<IChangeSkill>(out changeSkill))
            {
                Debug.Log("chageskill not found");
            }
            if (!spell.TryGetComponent<ISkillMove>(out skillMove))
            {
                Debug.Log("skillMove not found");
            }
            else
            {
                SendDirection(spell);
            }
            RetriveData(index);
            spell.transform.position = castingPoint.transform.position;
            if (currentSkill.HasEnoughMana(currentMana) && !currentSkill.IsOnCooldown())
            {
                currentSkill.UseMana(currentMana);
                Debug.Log($"Casting skill at index {index}, which is {currentSkill.GetType().Name}");
                currentSkill.UseSkill();
            }
            else
            {
                Debug.Log("Skill on coolDown");
            }
        }
        else
        {
            Debug.LogError("Skill index out of range");
        }
            
    }

    private void SendDirection(GameObject spell)
    {
        if (transform.parent.gameObject.transform.localScale.x == 1)
        {
            skillMove.ReciveDirection(1);
            spell.transform.localScale = new Vector3(5, 5, 5);
            Debug.Log("Player is looking Right");
        }
        else
        {
            skillMove.ReciveDirection(-1);
            spell.transform.localScale = new Vector3(-5,5,5);
            Debug.Log("Player is looking Left");
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
                Debug.Log($"Retrieved data for {skill.GetType().Name}: Mana = {skill.manaCost}, Cooldown = {skill.coolDownTime}, SpellSprite = {data.spellAnimation.name}");
                changeSkill.ReciveSprite(data.spellAnimation);
                Debug.Log($"skill sprite has changes to {data.spellAnimation.name}");
            }
        }
        
    }
}
