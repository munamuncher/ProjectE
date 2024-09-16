using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Player_Idle,
    Player_Move,
    Player_Attack,
    Player_UseSkill
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterControllers : MonoBehaviour
{

    private Rigidbody2D rgb;
    private CapsuleCollider2D ccd;

    private IMoveable moveable;
    private ITarget target;
    private IAnimations animations;
    private PlayerState currentState = PlayerState.Player_Idle;

    private void Awake()
    {
       if(!TryGetComponent<Rigidbody2D>(out rgb))
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
            ccd.offset = new Vector2 (0f, 0.7f);
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
    }

    private void Start()
    {
        if (moveable != null)
        {
            PlayerController(PlayerState.Player_Move);
        }
        else
        {
            Debug.Log("Moveable 참조 실패");
        }
    }
    private void Update()
    {
          moveable.Move();
    }

    private void PlayerMove()
    {

        Debug.Log("Playermove is being called");
        //if (currentState == PlayerState.Player_Move)
        //{
        //    PlayerController(PlayerState.Player_Move);
        //}
    }

    private void PlayerController(PlayerState playerState)
    {
        currentState = playerState;
        switch (playerState)
        {
            case PlayerState.Player_Move:
                animations.PlayMoveAnim("Move");
                StartCoroutine(StartMoving());
                break;
            case PlayerState.Player_Idle:
                StopCoroutine(StartMoving());
                animations.PlayMoveAnim("Idle");
                break;
        }

    }

    private IEnumerator StartMoving()
    {
        if(true)
        {
            PlayerMove();
            moveable.Move();
            Debug.Log("being called");
        }
       yield return new WaitForSeconds(1f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerController(PlayerState.Player_Idle);
        }
    }
}
