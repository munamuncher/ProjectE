using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour , IMoveable
{
    private float speed = 0.5f;
    private GameObject target;
    private float attackRange = 1.5f;

    private void Awake()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }    
    }


    public void Move()
    {
        if (target != null)
        {
            speed = 0.5f;
            Vector3 direction = target.transform.position - transform.position;
            float distance = direction.magnitude;
            if (distance > attackRange)
            {
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
