using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCustomize : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]
    private MonsterData monsterData;
    [SerializeField]
    private Transform bodyPartParent;

    public void MonsterCustom(int index)
    {
        if (index < 0 || index >= monsterData.monsterDataList.Count)
        {
            Debug.LogError("Index out of bounds - MonsterData List");
            return;
        }

        var currentMonster = monsterData.monsterDataList[index];

        EnableAndSetSprite("Head", currentMonster.HeadSprite);
        EnableAndSetSprite("Body", currentMonster.BodySprite);
        EnableAndSetSprite("ArmRight", currentMonster.RarmSprite);
        EnableAndSetSprite("ArmLeft", currentMonster.LarmSprite);
        EnableAndSetSprite("LegRight", currentMonster.RLegSprite);
        EnableAndSetSprite("LegLeft", currentMonster.LLegSprite);
    }

    void EnableAndSetSprite(string partName, Sprite sprite)
    {
        GameObject bodyPart = GetBodyPart(partName);

        if (bodyPart != null)
        {
            bool hasSprite = sprite != null;
            bodyPart.SetActive(hasSprite);
            if (hasSprite)
            {
                SpriteRenderer partRenderer = bodyPart.GetComponent<SpriteRenderer>();
                if (partRenderer != null)
                {
                    partRenderer.sprite = sprite;
                }
                else
                {
                    Debug.LogWarning($"SpriteRenderer not found on {partName}");
                }
            }
        }
        else
        {
            Debug.LogWarning($"{partName} body part not found");
        }
    }

    GameObject GetBodyPart(string partName)
    {
        foreach (Transform child in bodyPartParent)
        {
            if (child.name == partName)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}