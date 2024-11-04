using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Player_Idle,
    Player_Move,
    Player_Attack,
    Player_UseSkill,
    Player_Die
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterControllers : MonoBehaviour ,IDamageable
{

    private Rigidbody2D rgb;
    private CapsuleCollider2D ccd;

    private IMoveable moveable;
    private IAnimations animations;
    [SerializeField]
    private PlayerState currentState = PlayerState.Player_Idle;
    private IDamageable damageable;
    private ITarget target;
  
    private SkillManager skillManager;
    private float castingTime = 2f;

    private int Health = 1000;
    private int Amrmor = 10;

    private void Awake()
    {
        if (!TryGetComponent<Rigidbody2D>(out rgb))
        {
            Debug.LogError("Rigidbody2D 참조 실패");
        }
        else
        {
            rgb.simulated = true;
            rgb.gravityScale = 0f;
        }
        if (!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.LogError("PolygonCollider2D 참조 실패");
        }
        else
        {
            ccd.isTrigger = true;
            ccd.enabled = true;
            ccd.offset = new Vector2(0f, 0.7f);
            ccd.size = new Vector2(1f, 1.5f);
        }

        moveable = GetComponent<IMoveable>();
        if (moveable == null)
        {
            Debug.LogError("IMoveable 참조 실패");
        }
        animations = GetComponent<IAnimations>();
        if (animations == null)
        {
            Debug.LogError("IAnimations 참조 실패");
        }
        target = GetComponent<ITarget>();
        if(target == null)
        {
            Debug.LogError("ITarget 참조 실패");
        }
    }

    private void Start()
    {
        PlayerController(PlayerState.Player_Move); 
    }

    private void Update()
    {
        if (target.target != null && target.target.activeInHierarchy)
        {
            if (currentState == PlayerState.Player_Idle)
            {
                PlayerController(PlayerState.Player_Move);
            }
        }
        else
        {
            if(currentState == PlayerState.Player_Idle)
            {
                target.UpdateEnemyList();
                target.FindClosestTarget();
            }
        }
    }


    private void PlayerController(PlayerState playerState)
    {
        currentState = playerState;
        Debug.Log(playerState);
        switch (playerState)
        {
            case PlayerState.Player_Move:
                animations.PlayAnim("Move" , true);
                StartCoroutine(StartMoving());
                break;
            case PlayerState.Player_Idle:
                animations.PlayAnim("Idle" ,true);
                target.FindClosestTarget();
                break;
            case PlayerState.Player_Attack:
                StopCoroutine(StartMoving());
                StartCoroutine(StartAttacking());
                Debug.Log($"{currentState} is being called");
                break;
            case PlayerState.Player_UseSkill:
                StopCoroutine(StartMoving());
                animations.PlayAnim("Skill", false);
                break;
            case PlayerState.Player_Die:
                StopAllCoroutines();
                ccd.enabled = false;
                animations.PlayAnim("Dead", false);

                break;
        }
    }
    #region _SpellCasting_
    private void CastingSpell(int SkillIndex)
    {
        skillManager = SkillManager.sInst;
        skillManager.CastSpell(SkillIndex);
        PlayerController(PlayerState.Player_UseSkill);
        StartCoroutine(StartCastTIme());
    }
    private IEnumerator StartCastTIme()
    {
        yield return new WaitForSeconds(castingTime);
        CheckDeadOrAlive();
    }
    #endregion
    private IEnumerator StartAttacking()
    {
        while (currentState == PlayerState.Player_Attack)
        {
            if (target.target != null && target.target.activeInHierarchy)
            {
                var damageable = target.target.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    animations.PlayAnim("Attack", false);
                    yield return new WaitForSeconds(0.5f);
                    damageable.Damage(20);
                    Debug.Log($"Attacking target: {target.target}");
                }
                else
                {
                    Debug.Log($"{target.target} is Dead");
                    PlayerController(PlayerState.Player_Idle);
                    yield break;
                }
            }
            else
            {
                Debug.Log("Target is null, stopping attack.");
                PlayerController(PlayerState.Player_Idle);
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator StartMoving()
    {
        while (currentState == PlayerState.Player_Move)
        {
            if (target.target != null)
            {
                moveable.Move(target.target);
                AttackRange(target.target);
                Debug.Log($"StartMoving coroutine is running...... current{target.target}");
                yield return null;
            }
            else
            {
                Debug.Log("Target is null, stopping movement.");
                PlayerController(PlayerState.Player_Idle);
                yield break;
            }
        }
    }
    private void CheckDeadOrAlive()
    {
        if (target.target == null)
        {
            PlayerController(PlayerState.Player_Move);
            Debug.Log("target is Dead Looking for next");
        }
        else
        {
            PlayerController(PlayerState.Player_Attack);
        }
    }
    private void AttackRange(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;
        if(distance < 1.2f)
        {
            PlayerController(PlayerState.Player_Attack);
            Debug.Log($"{target} is in range");
        }
        else
        {
            Debug.Log($"{target} is not in range");
        }
    }

    public void Damage(int DamageAmount)
    {
        Health -= (DamageAmount - Amrmor);
        Debug.Log($"{Health}");
        animations.PlayAnim("Hit", false);
        if (Health <= 0)
        {
            PlayerController(PlayerState.Player_Die);
            Debug.Log("player is dead");
        }
    }
}
