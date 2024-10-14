using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//todo List
// check player death
// check for how much enemy and type there would be


public enum EnemyState
{ 
    Idle,
    Attack,
    Hit,
    Move,
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemeyController : PoolLabel , IDamageable
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int armor;
    [SerializeField]
    private int damage;

    private Rigidbody2D rbd;
    private CapsuleCollider2D ccd;
    private Animator anim;
    [SerializeField]
    private GameObject Player;

    private IMoveable moveable;
    private IAnimations animations;

    private void Awake()
    {
        if(!TryGetComponent<Rigidbody2D>(out rbd))
        {
            Debug.LogError("Rigidbody2d 참조 실패 - EnemyController.cs -  Awake()");
        }
        else
        {
            rbd.gravityScale = 0f;

        }
        if (!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.LogError("CapsuleCollider2D 참조 실패 - EnemyController.cs -  Awake()");
        }
        else
        {
            ccd.isTrigger = true;
            ccd.size = new Vector2(3f, 3f);
        }

        moveable = GetComponent<IMoveable>();
        if(moveable == null)
        {
            Debug.LogError("IMoveable 참조 실패 - EnemyController.cs - Awake()");
        }
        animations = GetComponent<IAnimations>();
        if(animations == null)
        {
            Debug.LogError("IAnimations 참조 실패 - EnemyController.cs - Awake()");
        }
    }

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("Animator 참조 실패 - EnemyController.cs - OnEnable");
        }
    }

    private void Update()
    {
        moveable?.Move(Player);
    }

    private void MonsterController(EnemyState enemyState)
    {
        Debug.Log(enemyState);
        switch (enemyState)
        {
            case EnemyState.Idle:
                animations.PlayAnim("idle", false);
                break;
            case EnemyState.Attack:
                animations.PlayAnim("attack", true);
                break;
            case EnemyState.Move:
                animations.PlayAnim("move", false);
                break;
            case EnemyState.Hit:
                break;
        }
    }

    public void ReciveStatus(int Health, int Amror, int Damage, RuntimeAnimatorController MonsterAnim)
    {
        this.health = Health;
        this.armor = Amror;
        this.damage = Damage;        
        Debug.Log($"i have recived {Health}, {Amror}, {Damage}");
        this.anim.runtimeAnimatorController = MonsterAnim;
        Debug.Log($"i have recived {MonsterAnim}");
    }

    #region _Monster_Colliders_
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MonsterController(EnemyState.Attack);
            Player = collision.gameObject; 
            Debug.Log($"Player is detected attacking {collision.gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            Debug.Log("Target lost: " + Player.name);
            Player = null;
            MonsterController(EnemyState.Move);
        }

    }


    #endregion    
    public void Damage(int DamageAmount)
    {
        health -= (DamageAmount - armor);
        Debug.Log($"i have Taken {DamageAmount} now {health} is remaining");
        if(health <= 0)
        {
            Push();
        }
    }
}
