using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    public float _health = 100;
    public bool IsDead{get; private set;} = false;
    public bool IsStunned{get; private set;} = false;
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health > 0) return;
        _health = 0;
        IsDead = true;
    }
    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }
    private IEnumerator StunCoroutine(float duration)
    {
        IsStunned = true;
        yield return new WaitForSecondsRealtime(duration);
        IsStunned = false;
    }
    private void Update() {
        if(Input.GetMouseButtonDown(1))
        {
            Stun(3f);
        }
    }
    public float GetHealth() => _health;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(Constant.EnemyBulletTag))
        {
            TakeDamage(10);
            Destroy(other.gameObject);
        }
    }
}