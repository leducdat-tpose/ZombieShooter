using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private int _heartPoint;
    public bool IsDead{get; private set;} = false;
    public bool IsStunned{get; private set;} = false;
    public void TakeDamage(int damage)
    {
        _heartPoint -= damage;
        if(_heartPoint > 0) return;
        _heartPoint = 0;
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
}
