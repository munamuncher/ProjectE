using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PoolLabel targetLabel;
    [SerializeField]
    private int allocateCount;

    private Stack<PoolLabel> poolStack = new Stack<PoolLabel>();

    private void Awake()
    {
        Allocate();
    }

    private void Allocate()
    {
        for(int i =0;i<allocateCount;i++)
        {
            PoolLabel label = Instantiate(targetLabel, transform);
            label.Create(this);
            poolStack.Push(label);
        }
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
}
