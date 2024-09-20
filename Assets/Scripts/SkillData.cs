using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill" , menuName = "Skill / Skills" )]
public class SkillData : ScriptableObject
{
    public string skillName;
    public float mana;
    public float coolDown;
    public string skillDescription;
    //maybe icon for sprites for effectioncy
}
