using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour, ITarget
{
    public List<GameObject> allObjects = new List<GameObject>();
    public GameObject nearTarget;
    private float nearestDistance = float.MaxValue;
    public GameObject target { get; set; }

    public void UpdateEnemyList() //리스트가 비여있을때 찾고 
    {
        allObjects.Clear();
        allObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public void FindClosestTarget()//제일 가까운 적을 타켓으로 지정
    {
        UpdateEnemyList();
        nearestDistance = float.MaxValue;
        foreach (GameObject obj in allObjects)
        {
            if (obj != null && obj.activeInHierarchy)
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

