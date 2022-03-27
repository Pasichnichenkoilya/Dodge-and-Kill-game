using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public PooledObjectTag tag;
    public GameObject prefab;
    public int defaultCapacity;
    public int maxSize;
}