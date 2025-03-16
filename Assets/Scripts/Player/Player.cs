using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public interface IDamageable{
    public void TakeDamage(float damage);
    public void Healing(float healthAmount);
}

public class Player : MonoBehaviour, IDamageable
{
    [Header("Attributes")]
    [SerializeField]
    private float _maxHealth = 100;
    private float _currentHealth;
    [SerializeField]
    private float _invincibleTime = 2f;
    public Inventory Inventory{get; private set;} = new Inventory();
    public bool IsInvincible{get; private set;}
    public bool IsDead{get; private set;}
    public bool IsStunned{get; private set;}
    public event Action OnPlayerDataChanged;
    public void Initialise()
    {
        _currentHealth = _maxHealth;
        IsInvincible = false;
        IsDead = false;
        IsStunned = false;
    }
    public void TakeDamage(float damage)
    {
        if(IsInvincible || IsDead) return;
        _currentHealth -= damage;
        OnPlayerDataChanged?.Invoke();
        StartCoroutine(InvincibleCoroutine());
        if(_currentHealth > 0) return;
        _currentHealth = 0;
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
    public void Healing(float healthAmount)
    {
        _currentHealth += healthAmount;
        if(_currentHealth > _maxHealth) _currentHealth = _maxHealth;
        OnPlayerDataChanged?.Invoke();
    }
    public float GetMaxHealth() => _maxHealth;
    public float GetCurrentHealth() => _currentHealth;
}