using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour , IRecivePoolObjects
{
    public PoolLabel targetLabel;
    [SerializeField]
    private int allocateCount;

    private Stack<PoolLabel> poolStack = new Stack<PoolLabel>();
    private void Awake()
    {
        Allocate();
    }
    private void Allocate()
    {
        if (targetLabel != null)
        {
            for (int i = 0; i < allocateCount; i++)
            {
                PoolLabel label = Instantiate(targetLabel, transform);
                label.Create(this);
                poolStack.Push(label);
            }
        }
        else
        {
            Debug.LogWarning("no Target is in the targetLabel");
        }
    }
        public void ClearPool()
    {
        while (poolStack.Count > 0)
        {
            PoolLabel label = poolStack.Pop();
            Destroy(label.gameObject);
        }
        poolStack.Clear();
    }
    PoolLabel Plabel;
    public GameObject Pop()
    {
        Plabel = poolStack.Pop();
        Plabel.gameObject.SetActive(true);
        return Plabel.gameObject;
    }

    public void Push(PoolLabel returnLabel)
    {
       returnLabel.gameObject.SetActive(false);
       poolStack.Push(returnLabel);
    }

    public void ReciveGameObject(GameObject Objects)
    {
        targetLabel = Objects.GetComponent<PoolLabel>();        
        ClearPool();
        Allocate();

    }
}
