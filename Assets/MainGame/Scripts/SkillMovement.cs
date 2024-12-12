using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SkillMovement : PoolLabel, ISkillMove, IChangeSkill
{
    private float DeathTimer = 3f;

    private CapsuleCollider2D ccd;
    private Rigidbody2D rrd;

    [SerializeField]
    private SpriteRenderer skillSprite;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private int finalDamage;
    private int direction;

    private IDamageable damage;
    private ISkillDamageCalc skillDamageCalc;

    private void Awake()
    {
        if(!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.LogError("CapsuleCollider2D 참조 실패 - SkillMovement.cs - Awake()");
        }
        if(!TryGetComponent<Rigidbody2D>(out rrd))
        {
            Debug.LogError("Rigidbody2D 참조 실패 - skillMovement.cs - Awake()");
        }
    }
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

    public void ReciveDamageData(int Ddata)
    {
        finalDamage = Ddata;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.activeInHierarchy)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out damage))
            {
                damage.Damage(finalDamage);
                Debug.LogWarning($"doing {finalDamage}");
            }
            else
            {
                Debug.LogError("IDamageable component not found on Enemy.");
            }
        }
    }
}
