using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{ 
    Idle,
    Attack,
    Hit,
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemeyController : MonoBehaviour
{
    private Rigidbody2D rbd;
    private CapsuleCollider2D ccd;

    private IMoveable moveable;

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
            ccd.size = new Vector2(1.6f, 2.5f);
        }

        moveable = GetComponent<IMoveable>();
        if(moveable == null)
        {
            Debug.LogError("IMoveable 참조 실패 - EnemyController.cs - Awake()");
        }    
    }

    private void Update()
    {
        moveable?.Move();
    }
}
