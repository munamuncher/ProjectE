using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMovement : PoolLabel, ISkillMove, IChangeSkill
{
    private float DeathTimer = 3f;
    [SerializeField]
    private SpriteRenderer skillSprite;

    private void OnEnable()
    {
        DeathTimer = 3f;
    }

    public void Update()
    {
        SkillMove();
    }

    public void ReciveSprite(Sprite sprite)
    {
        skillSprite.sprite = sprite;        
        Debug.Log($"sprite has recived the change {sprite.name}");
    }
    public void SkillMove()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 10f);
        DeathTimer -= Time.deltaTime;
        if (DeathTimer <= 0)
        {
            Push();
        }
    }
}
