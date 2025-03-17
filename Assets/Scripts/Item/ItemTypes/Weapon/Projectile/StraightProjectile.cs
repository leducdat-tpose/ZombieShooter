using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : Projectile
{
    [SerializeField]
    private float _moveSpeed = 20f;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    public override void Initialise(float damage, Vector2 direction, WeaponData.SpecialAbility ability = WeaponData.SpecialAbility.None)
    {
        animator.Play("Default");
        this.damage = damage;
        this.direction = direction;
        this.ability = ability;
    }

    protected override void Move()
    {
        transform.Translate(direction * _moveSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate() {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        OnHit(other);
    }

    protected override void OnHit(Collider2D other)
    {
        if(this.CompareTag(Constant.EnemyBulletTag))
        {
            if(other.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(damage);
                if(ability == WeaponData.SpecialAbility.Stun) player.Stun(1f);
            }
        }
        if(other.CompareTag(Constant.StaticObject))
        {
        }
        if(!this.CompareTag(Constant.EnemyBulletTag))
        {
            if(other.TryGetComponent<Monster>(out Monster monster))
            {
                monster.TakeDamage(damage);
            }
        }
        StartCoroutine(ReturnPoolAfterHit());
    }
    private IEnumerator ReturnPoolAfterHit()
    {
        animator.SetTrigger("Hit");
        direction = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        GetComponent<PooledObject>().ReturnToPool();
    }
    public override void OnObjectSpawn()
    {
        animator.Play("Default");
    }
}
