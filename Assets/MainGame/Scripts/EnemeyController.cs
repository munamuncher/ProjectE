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

    private EnemyState currentEnemyState;

    private Rigidbody2D rbd;
    private CapsuleCollider2D ccd;
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject body;

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
        if (body != null)
        {
            if(!body.TryGetComponent<SpriteRenderer>(out sr))
            {
                Debug.LogError("SpriteRenderer 참조 실패 - EnemyController.cs - Awake()");
            }
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
        currentEnemyState = enemyState;
        switch (enemyState)
        {
            case EnemyState.Idle:
                animations.PlayAnim("idle", false);
                break;
            case EnemyState.Attack:
                StartCoroutine(MonStartAttacking());
                break;
            case EnemyState.Move:
                animations.PlayAnim("move", false);
                break;
            case EnemyState.Hit:
                break;
        }
    }

    private IEnumerator MonStartAttacking()
    {
        while (currentEnemyState == EnemyState.Attack)
        {
            if (Player != null && Player.gameObject.activeInHierarchy)
            {
                var playerDamageable = Player.GetComponent<IDamageable>();
                if(playerDamageable != null)
                {
                    animations.PlayAnim("attack", false);
                    yield return new WaitForSeconds(1.5f);
                    playerDamageable.Damage(damage);
                }
                else
                {
                    Debug.Log($"{Player.gameObject.name} is Dead");
                    MonsterController(EnemyState.Idle);
                    yield break;
                }
            }
        }
    }

    public void ReciveStatus(int Health, int Amror, int Damage)
    {
        this.health = Health;
        this.armor = Amror;
        this.damage = Damage;        
        //Debug.Log($"i have recived {Health}, {Amror}, {Damage}");
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
            MonsterController(EnemyState.Idle);
        }

    }

    #endregion    
    public void Damage(int DamageAmount)
    {
        health -= (DamageAmount - armor);
        Debug.Log($"i have Taken {DamageAmount} now {health} is remaining");
        StartCoroutine(takeDamge());
        if(health <= 0)
        {           
            Debug.Log($"this {gameObject.name} is dead pushing");
            GameManager.GMInst.DeathNotice();
            Push();

        }
    }

    private IEnumerator takeDamge()
    {
        if(body !=  null && sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            sr.color = Color.white;
            yield break;
        }
    }
}
