using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private float speed = 3f;
    public void Move(GameObject target)
    {
        if (target != null)
        {
            speed = 3f;
            Vector3 direction = target.transform.position - transform.position;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position - new Vector3(1f, 0.5f, 0f), speed * Time.deltaTime);
                Debug.Log("enemy on the right");
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position - new Vector3(-1f, 0.5f, 0f), speed * Time.deltaTime);
                Debug.Log("enemy on the Left");
            }
        }
    }
}
