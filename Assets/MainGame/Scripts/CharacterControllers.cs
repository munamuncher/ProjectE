using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Player_Idle,
    Player_Move,
    Player_Attack,
    Player_UseSkill,
    Player_Die,
    Player_Hit
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterControllers : MonoBehaviour
{

    private Rigidbody2D rgb;
    private CapsuleCollider2D ccd;

    private IMoveable moveable;
    private IAnimations animations;
    [SerializeField]
    private PlayerState currentState = PlayerState.Player_Idle;
    private IDamageable damageable;
    private ITarget target;

    private GameObject detectedTarget;
  
    private SkillManager skillManager;
    private float castingTime = 2f;
     

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
        if(Input.GetKeyDown(KeyCode.K))
        {
            CastingSpell(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerController(PlayerState.Player_Die);
        }
    }

    private void CastingSpell(int SkillIndex)
    {
        skillManager = SkillManager.sInst;
        skillManager.CastSpell(SkillIndex);
        PlayerController(PlayerState.Player_UseSkill);
        StartCoroutine(StartCastTIme());
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
                animations.PlayAnim("Dead", false);
                break;
            case PlayerState.Player_Hit:
                animations.PlayAnim("Hit", false);
                break;
        }
    }

    private IEnumerator StartCastTIme()
    {
        yield return new WaitForSeconds(castingTime);
        CheckDeadOrAlive();
    }

    private IEnumerator StartAttacking()
    {
        while (currentState == PlayerState.Player_Attack)
        {
            animations.PlayAnim("Attack", false);
            if(target.target != null)
            {
                target.target.GetComponent<IDamageable>().Damage(20);
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
                Debug.Log("StartMoving coroutine is running");
                yield return null;
            }
            else
            {
                Debug.Log("Target is null, stopping movement.");
                yield break;
            }
        }
    }

    private void CheckDeadOrAlive()
    {
        if (detectedTarget == null)
        {
            PlayerController(PlayerState.Player_Move);
            Debug.Log("target is Dead Looking for next");
        }
        else
        {
            PlayerController(PlayerState.Player_Attack);
        }
    }
    //공격을 할때 코로틴 사용으로 변경
    #region _Player_Colliders_ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.target)
        {
            PlayerController(PlayerState.Player_Attack);
            Debug.Log("Attacker in range");
        }
    }
    #endregion
}
