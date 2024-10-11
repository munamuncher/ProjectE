using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour, ITarget
{
    public List<GameObject> allObjects = new List<GameObject>();
    public GameObject nearTarget;
    private float nearestDistance = float.MaxValue;
    public GameObject target { get; set; }

    public void UpdateEnemyList() //����Ʈ�� �������� ã�� 
    {
        allObjects.Clear();
        allObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public void FindClosestTarget()//���� ����� ���� Ÿ������ ����
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

