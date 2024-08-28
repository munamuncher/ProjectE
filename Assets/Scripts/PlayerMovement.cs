using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveable, ITarget
{
    [SerializeField] private float speed = 3f;

    public GameObject target { get; set; }

    public void Move()
    {
        if (target == null)
        {
            Debug.Log("Nothing");
            return;
        }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(target.transform.position);

        Debug.Log(target);
    }
}
