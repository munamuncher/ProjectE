using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour, ITarget
{
    public List<GameObject> allObjects = new List<GameObject>();
    public GameObject nearTarget;
    private float nearestDistance = float.MaxValue;

    public GameObject target { get; set; }

    private void Awake()
    {
        UpdateEnemyList();
        FindClosestTarget();
    }

    private void Update()
    {
        if (nearTarget == null || !nearTarget.activeInHierarchy)
        {
            UpdateEnemyList();
            FindClosestTarget();
        }
       
    }

    private void UpdateEnemyList()
    {
        allObjects.Clear();
        allObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void FindClosestTarget()
    {
        nearestDistance = float.MaxValue;

        foreach (GameObject obj in allObjects)
        {
            if (obj != null) 
            {
                float distance = Vector3.Distance(this.transform.position, obj.transform.position);
                if (distance < nearestDistance)
                {
                    nearTarget = obj;
                    nearestDistance = distance;

                }
            }
        }                   
        target = nearTarget;
    }


}

