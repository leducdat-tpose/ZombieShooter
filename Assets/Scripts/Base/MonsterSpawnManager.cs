using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _monsterPrefabs;
    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private int _maxMonstersPerRoom = 30;
    [SerializeField]
    private float _spawnInterval = 40f;
    private List<SpawnZone> _spawnZones = new List<SpawnZone>();
    private float _nextSpawnTime;
    private ObjectPool _objectPool;
    private void Start() {
        _spawnZones.AddRange(FindObjectsOfType<SpawnZone>());
        _objectPool = ObjectPool.Instance;
        foreach(GameObject monsterPrefab in _monsterPrefabs)
        {
            _objectPool.CreatePool(monsterPrefab, 50);
        }
        _objectPool.CreatePool(_bossPrefab, 5);
        SpawnMonstersInAllRooms();
    }
    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnMonstersInAllRooms();
            _nextSpawnTime = Time.time + _spawnInterval;
        }
    }
    public void SpawnMonstersInAllRooms()
    {
        foreach (SpawnZone zone in _spawnZones)
        {
            SpawnMonstersInRoom(zone);
        }
    }
    private void SpawnMonstersInRoom(SpawnZone zone)
    {
        int currentMonster = zone.GetCurrentMonsterCount();
        int roomSize = Mathf.CeilToInt(zone.GetAreaSize() / 10f);
        int monstersToSpawn = Mathf.Min(_maxMonstersPerRoom, roomSize) - currentMonster;
        for (int i = 0; i < monstersToSpawn; i++)
        {
            Vector2 spawnPosition = zone.GetRandomPointInside();
            GameObject randomMonster = _monsterPrefabs[Random.Range(0, _monsterPrefabs.Length)];
            GameObject monster = _objectPool.GetObject(randomMonster, spawnPosition, Quaternion.identity);
            zone.RegisterMonster(monster);
        }
        if(zone.HaveBoss)
        {
            Vector2 spawnPosition = zone.gameObject.transform.position;
            GameObject monster = _objectPool.GetObject(_bossPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
