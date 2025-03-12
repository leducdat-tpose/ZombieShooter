using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float damage;
    protected Vector2 direction;
    public abstract void Initialise(float damage, Vector2 direction);
    protected abstract void Move();
    protected abstract void OnHit(Collider2D other);
}