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
public class EnemeyController : MonoBehaviour
{
    private Rigidbody2D rbd;
    private CapsuleCollider2D ccd;
    [SerializeField]
    private GameObject Player;

    private IMoveable moveable;
    private IAnimations animations;

    private void Awake()
    {
        if(!TryGetComponent<Rigidbody2D>(out rbd))
        {
            Debug.LogError("Rigidbody2d ���� ���� - EnemyController.cs -  Awake()");
        }
        else
        {
            rbd.gravityScale = 0f;

        }
        if (!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.LogError("CapsuleCollider2D ���� ���� - EnemyController.cs -  Awake()");
        }
        else
        {
            ccd.isTrigger = true;
            ccd.size = new Vector2(1.6f, 2.5f);
        }

        moveable = GetComponent<IMoveable>();
        if(moveable == null)
        {
            Debug.LogError("IMoveable ���� ���� - EnemyController.cs - Awake()");
        }
        animations = GetComponent<IAnimations>();
        if(animations == null)
        {
            Debug.LogError("IAnimations ���� ���� - EnemyController.cs - Awake()");
        }
    }

    private void Update()
    {
        moveable?.Move();
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
}
