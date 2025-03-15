using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IDamageable{
    public void TakeDamage(float damage);
}

public class Player : MonoBehaviour, IDamageable
{
    [Header("Attributes")]
    [SerializeField]
    private float _health = 100;
    [SerializeField]
    private float _invincibleTime = 2f;
    public Inventory Inventory{get; private set;} = new Inventory();
    public bool IsInvincible{get; private set;} = false;
    public bool IsDead{get; private set;} = false;
    public bool IsStunned{get; private set;} = false;
    public void TakeDamage(float damage)
    {
        if(IsInvincible || IsDead) return;
        Debug.Log("take damage");
        _health -= damage;
        StartCoroutine(InvincibleCoroutine());
        if(_health > 0) return;
        _health = 0;
        IsDead = true;
    }
    private IEnumerator InvincibleCoroutine()
    {
        IsInvincible = true;
        yield return new WaitForSecondsRealtime(_invincibleTime);
        IsInvincible = false;
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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            foreach(KeyValuePair<ItemData, int> keyValuePair in Inventory.GetData())
            {
                Debug.Log($"Item data: {keyValuePair.Key}, item name: {keyValuePair.Key.ItemName}, amount: {keyValuePair.Value}");
            }
        }
    }
    public float GetHealth() => _health;
}