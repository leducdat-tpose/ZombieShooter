using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IPoolable
{
    protected float damage;
    protected Vector2 direction;
    protected Animator animator;
    protected WeaponData.SpecialAbility ability;
    public abstract void Initialise(float damage, Vector2 direction, WeaponData.SpecialAbility ability = WeaponData.SpecialAbility.None);

    public virtual void OnObjectSpawn(){}

    protected abstract void Move();
    protected abstract void OnHit(Collider2D other);
}