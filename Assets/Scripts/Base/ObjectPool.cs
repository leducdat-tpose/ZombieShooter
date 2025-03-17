using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable{
    void OnObjectSpawn();
}

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject poolObj = new GameObject("ObjectPool");
                _instance = poolObj.AddComponent<ObjectPool>();
            }
            return _instance;
        }
    }
    private Dictionary<string, Queue<GameObject>> _poolDictionary = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> _prefabDictionary = new Dictionary<string, GameObject>();
    private Dictionary<string, Transform> _poolParentDictionary = new Dictionary<string, Transform>();
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void CreatePool(GameObject prefab, int initialAmount)
    {
        string prefabName = prefab.name;
        if(_poolDictionary.ContainsKey(prefabName))
        {
            Debug.LogWarning($"Pool for {prefabName} already exists!");
            return;
        }
        GameObject poolParent = new GameObject($"{prefabName}_Pool");
        poolParent.transform.parent = this.transform;
        _poolParentDictionary[prefabName] = poolParent.transform;
        Queue<GameObject> objectPool = new Queue<GameObject>();
        _prefabDictionary[prefabName] = prefab;
        for(int i = 0; i < initialAmount; i++)
        {
            GameObject obj = CreateNewObject(prefabName);
            objectPool.Enqueue(obj);
        }
        _poolDictionary[prefabName] = objectPool;
    }
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        string prefabName = prefab.name;
        if (!_poolDictionary.ContainsKey(prefabName))
        {
            CreatePool(prefab, 10);
        }
        if (_poolDictionary[prefabName].Count == 0)
        {
            GameObject obj = CreateNewObject(prefabName);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        GameObject pooledObject = _poolDictionary[prefabName].Dequeue();
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;
        pooledObject.SetActive(true);
        IPoolable[] poolables = pooledObject.GetComponents<IPoolable>();
        foreach (var poolable in poolables)
        {
            poolable.OnObjectSpawn();
        }
        
        return pooledObject;
    }
    public void ReturnToPool(GameObject obj)
    {
        string prefabName = obj.name.Replace("(Clone)", "").Trim();
        
        if (!_poolDictionary.ContainsKey(prefabName))
        {
            Debug.LogWarning($"Trying to return {obj.name} to pool, but no pool exists for {prefabName}");
            Destroy(obj);
            return;
        }
        
        obj.SetActive(false);
        _poolDictionary[prefabName].Enqueue(obj);
    }
    private GameObject CreateNewObject(string prefabName)
    {
        GameObject obj = Instantiate(_prefabDictionary[prefabName]);
        obj.name = $"{prefabName}(Clone)";
        obj.transform.parent = _poolParentDictionary[prefabName];
        obj.SetActive(false);
        
        // Add PooledObject component to handle automatic return
        PooledObject pooledObj = obj.AddComponent<PooledObject>();
        pooledObj.Pool = this;
        
        return obj;
    }
    public void ClearAllPools()
    {
        foreach (var poolParent in _poolParentDictionary.Values)
        {
            Destroy(poolParent.gameObject);
        }
        
        _poolDictionary.Clear();
        _prefabDictionary.Clear();
        _poolParentDictionary.Clear();
    }
}

public class PooledObject : MonoBehaviour
{
    [HideInInspector]
    public ObjectPool Pool;
    public float LifeTime = -1;
    
    private float _spawnTime;
    
    private void OnEnable()
    {
        _spawnTime = Time.time;
    }
    
    private void Update()
    {
        if (LifeTime > 0 && Time.time > _spawnTime + LifeTime)
        {
            ReturnToPool();
        }
    }
    
    public void ReturnToPool()
    {
        if (Pool != null)
        {
            Pool.ReturnToPool(gameObject);
        }
    }
}