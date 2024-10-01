using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillMovement : PoolLabel, ISkillMove, IChangeSkill
{
    private float DeathTimer = 3f;
    [SerializeField]
    private SpriteRenderer skillSprite;
    [SerializeField]
    private Animator anim;
    private int direction;
    private void OnEnable()
    {
        DeathTimer = 2f;
    }

    public void Update()
    {
        SkillMove();
    }

    public void ReciveSprite(RuntimeAnimatorController animator)
    {
        anim.runtimeAnimatorController = animator;        
        Debug.Log($"sprite has recived the change {animator.name}");
    }
    public void ReciveDirection(int direct)
    {
        direction = direct;
    }
    public void SkillMove()
    {
        transform.Translate(Vector3.right * direction * Time.deltaTime * 10f);
        DeathTimer -= Time.deltaTime;
        if (DeathTimer <= 0)
        {
            Push();
        }
    }
}
