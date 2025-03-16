using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : Projectile
{
    private float _moveSpeed;
    [SerializeField]
    private float _maxMoveSpeed;
    [SerializeField]
    private float _explodeRadius;
    [SerializeField]
    private ContactFilter2D _contactFilter;
    Collider2D[] _affectedColliders = new Collider2D[25];
    [SerializeField]
    private float _force = 100;
    private bool _isExplode = false;
    private AnimationCurve _pathCurve;
    private AnimationCurve _axisCorrectionCurve;
    private AnimationCurve _speedCurve;
    private Vector2 _startPosition;
    private float _maxRelativeHeight;
    public override void Initialise(float damage, Vector2 direction)
    {
        _moveSpeed = _maxMoveSpeed;
        _startPosition = transform.position;
        this.damage = damage;
        this.direction = direction;
        float distanceX = direction.x - transform.position.x;
        _maxRelativeHeight = Mathf.Abs(distanceX) * _maxRelativeHeight;
    }
    public void SetCurves(AnimationCurve pathCurve, AnimationCurve axisCorrectionCurve, AnimationCurve speedCurve, float maxRelativeHeight)
    {
        _pathCurve = pathCurve;
        _axisCorrectionCurve = axisCorrectionCurve;
        _speedCurve = speedCurve;
        _maxRelativeHeight = maxRelativeHeight;
    }

    private void FixedUpdate() {
        Move();
    }

    protected override void Move()
    {
        if(_isExplode) return;
        Vector2 distance = direction - _startPosition;
        
        if(Mathf.Abs(distance.x) <= 3.0f)
        {
            transform.Translate(distance*_moveSpeed* Time.fixedDeltaTime);
        }
        else
        {
            if(direction.x < 0) _moveSpeed = -_moveSpeed;
            float nextPositionX = transform.position.x + _moveSpeed * Time.fixedDeltaTime;
            float nextPositionXNormalize = (nextPositionX - _startPosition.x)/distance.x;
            HandleSpeed(nextPositionXNormalize);
            float nextPositionYCorrectionNormalize = _axisCorrectionCurve.Evaluate(nextPositionXNormalize);
            float nextPositionYCorrection = nextPositionYCorrectionNormalize * distance.y;
            float nextPositionYNormalize = _pathCurve.Evaluate(nextPositionXNormalize);
            float nextPositionY = _startPosition.y + nextPositionYNormalize * _maxRelativeHeight + nextPositionYCorrection;
            Vector3 nextPosition = new Vector3(nextPositionX, nextPositionY);
            transform.position = nextPosition;
        }
        if(Vector2.Distance(direction, transform.position) < 0.1)
        {
            Explode();
        }
    }

    void Explode()
    {
        _isExplode = true;
        int numColliders = Physics2D.OverlapCircle(transform.position, _explodeRadius, _contactFilter, _affectedColliders);

        if (numColliders > 0)
        {
            for (int i = 0; i < numColliders; i++)
            {
                Debug.Log($"Effected obj: {_affectedColliders[i].gameObject.name}");
                if (_affectedColliders[i].gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    Vector3 forceDirection = rb.transform.position - transform.position;
                    float distanceModifier = 1 - (Mathf.Clamp(forceDirection.magnitude, 0, _explodeRadius) / _explodeRadius);
                    Vector2 forcePosition = _affectedColliders[i].ClosestPoint(transform.position);
                    rb.AddForceAtPosition((forceDirection * _force) * distanceModifier, forcePosition);
                }
                if(_affectedColliders[i].gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
        StartCoroutine(StartReturnPoolAfterExplode());
    }

    private IEnumerator StartReturnPoolAfterExplode()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<PooledObject>().ReturnToPool();
    }

    private void HandleSpeed(float nextPositionXNormalize)
    {
        _moveSpeed = _speedCurve.Evaluate(nextPositionXNormalize) * _maxMoveSpeed;
    }

    protected override void OnHit(Collider2D other)
    {
    }
    public override void OnObjectSpawn()
    {
        _isExplode = false;
    }
}
