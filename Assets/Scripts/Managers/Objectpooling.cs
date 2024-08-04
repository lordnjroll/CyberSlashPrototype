using System;
using System.Collections.Generic;
using UnityEngine;

public class Objectpooling : MonoBehaviour
{
    public static Objectpooling Instance;

    [SerializeField] private ObjectPoolItem[] _objectPoolItems;

    private readonly Dictionary<string, ObjectPoolItem> _objectPoolItemsDict =
        new Dictionary<string, ObjectPoolItem>();

    private readonly Dictionary<string, Queue<GameObject>> _objectPool =
        new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (var item in _objectPoolItems)
        {
            Debug.Assert(
                item.prefab,
                $"{name}: There has unassigned prefab in the pool items");

            var itemName = item.name;
            var queue = new Queue<GameObject>();

            _objectPoolItemsDict[itemName] = item;
            _objectPool[itemName] = queue;

            for (var i = 0; i < item.initialNum; ++i)
            {
                var obj = SpawnObject(itemName);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
        }
    }

    private GameObject SpawnObject(string poolItemName)
    {
        var prefab = _objectPoolItemsDict[poolItemName].prefab;
        var obj = Instantiate(prefab);
        obj.name = poolItemName;
        return obj;
    }

    public GameObject GetObject(string poolItemName)
    {
        var objQueue = _objectPool[poolItemName];
        return objQueue.Count > 0 ?
            objQueue.Dequeue() :
            SpawnObject(poolItemName);
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        if (obj.activeSelf)
            obj.SetActive(false);

        _objectPool[obj.name].Enqueue(obj);
    }
}
[Serializable]
public class ObjectPoolItem
{
    [SerializeField, HideInInspector]
    [Tooltip("The name of the pool item")]
    private string _name;
    [SerializeField]
    [Tooltip("The prefab for spawning objects in the pool")]
    private GameObject _prefab;
    [SerializeField, Min(0)]
    [Tooltip("The initial number of objects in the pool")]
    private int _initialNum;

    public string name => _name;
    public GameObject prefab => _prefab;
    public int initialNum => _initialNum;
}

