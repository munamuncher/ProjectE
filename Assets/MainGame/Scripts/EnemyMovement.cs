using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour , IMoveable
{
    private float speed =1.0f;
    private GameObject target;


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
            speed = 3f;
            Vector3 direction = target.transform.position - transform.position;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position - new Vector3(1f, -0.5f, 0f), speed * Time.deltaTime);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position - new Vector3(-1f, -0.5f, 0f), speed * Time.deltaTime);
            }
        }
    }
}
