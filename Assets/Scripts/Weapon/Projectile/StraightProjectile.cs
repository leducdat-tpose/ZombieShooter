using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : Projectile
{
    [SerializeField]
    private float _moveSpeed = 20f;
    public override void Initialise(float damage, Vector2 direction)
    {
        this.damage = damage;
        this.direction = direction;
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
                Destroy(this.gameObject);
            }
        }
        else if(other.CompareTag(Constant.StaticObject))
        {
            Destroy(this.gameObject);
        }
        else
        {
            if(other.TryGetComponent<Monster>(out Monster monster))
            {
                monster.TakeDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
