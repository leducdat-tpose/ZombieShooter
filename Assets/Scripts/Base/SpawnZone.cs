using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField]
    private Color _gizmoColor = new Color(1f, 0f, 0f, 0.2f);
    private BoxCollider2D _collider;
    [field: SerializeField]
    public bool HaveBoss{get; private set;}
    private List<GameObject> _monstersInZone = new List<GameObject>();
    private void Awake() {
        _collider = GetComponent<BoxCollider2D>();
        if(_collider != null) _collider.isTrigger = true;
    }
    public Vector2 GetRandomPointInside()
    {
        if(_collider == null) return transform.position;
        Bounds bounds = _collider.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x,y);
    }
    public float GetAreaSize()
    {
        if (_collider == null) return 1f;
        return _collider.size.x * _collider.size.y;
    }
    public void RegisterMonster(GameObject monster)
    {
        if (!_monstersInZone.Contains(monster))
        {
            _monstersInZone.Add(monster);
            Monster monsterComponent = monster.GetComponent<Monster>();
            if (monsterComponent != null)
            {
                monsterComponent.OnMonsterDeath += () => RemoveMonster(monster);
            }
        }
    }
    public void RemoveMonster(GameObject monster)
    {
        _monstersInZone.Remove(monster);
    }
    public int GetCurrentMonsterCount()
    {
        _monstersInZone.RemoveAll(monster => monster == null);
        return _monstersInZone.Count;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Constant.MonsterTag))
        {
            RemoveMonster(other.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        if (_collider == null) _collider = GetComponent<BoxCollider2D>();
        if (_collider != null)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawCube(transform.position, _collider.size);
        }
    }
}
