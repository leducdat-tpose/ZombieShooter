using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : Projectile
{
    private float _moveSpeed;
    [SerializeField]
    private float _maxMoveSpeed;
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
        Vector2 distance = direction - _startPosition;
        if(direction.x < 0) _moveSpeed = -_moveSpeed;
        float nextPositionX = transform.position.x + _moveSpeed * Time.fixedDeltaTime;
        float nextPositionXNormalize = (nextPositionX - _startPosition.x)/distance.x;
        HandleSpeed(nextPositionXNormalize);
        float nextPositionYCorrectionNormalize = _axisCorrectionCurve.Evaluate(nextPositionXNormalize);
        float nextPositionYCorrection = nextPositionYCorrectionNormalize * distance.y;
        float nextPositionYNormalize = _pathCurve.Evaluate(nextPositionXNormalize);
        float nextPositionY = _startPosition.y + nextPositionYNormalize * 3f + nextPositionYCorrection;
        Vector3 nextPosition = new Vector3(nextPositionX, nextPositionY);
        transform.position = nextPosition;
    }

    private void HandleSpeed(float nextPositionXNormalize)
    {
        _moveSpeed = _speedCurve.Evaluate(nextPositionXNormalize) * _maxMoveSpeed;
    }

    protected override void OnHit(Collider2D other)
    {
    }

    private void HandleProjectilePath()
    {
        
    }

}
