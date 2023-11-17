using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PoolerManager : MonoBehaviour
{
    [Header("Enemy")]
    public ObjectPool[] objectPool;
    private readonly List<ObjectPool> commonEnemies = new List<ObjectPool>();

    private void Awake()
    {
        // Enemy
        for (int i = 0; i < objectPool.Length; i++)
        {
            Assert.IsNotNull(objectPool[i]);
            commonEnemies.Add(objectPool[i]);
        }
    }

    private void Start()
    {
        // Enemy
        foreach (var pool in this.GetComponentsInChildren<ObjectPool>())
            pool.Initialize();
    }

    // Enemy
    public GameObject CallSuika(SuikaType suikaName)
    {
        GameObject objectPooled = GetSuika((SuikaType)((int)suikaName)).GetObject();
        return objectPooled;
    }

    public ObjectPool GetSuika(SuikaType suikaType)
    {
        return commonEnemies[(int)suikaType];
    }
}
