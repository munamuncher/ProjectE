using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private float speed = 3f;
    private TargetTracker tracker;
    private GameObject target;

    private void Awake()
    {
       if(!TryGetComponent<TargetTracker>(out tracker))
        {
            Debug.LogError("TargetTracker.cs  참조 실패 - playerMovement.cs - Awake()");
        }
    }

    private void Update()
    {
        if (tracker != null && tracker.target != null)
        {

            target = tracker?.target;
        }
    }

    public void Move()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position - new Vector3(1f, 0.5f, 0f), speed * Time.deltaTime);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position - new Vector3(1f, 0.5f, 0f), speed * Time.deltaTime);
            }
        }
    }
}
