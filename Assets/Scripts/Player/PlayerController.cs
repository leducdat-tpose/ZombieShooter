using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Player _playerStatus;
    [SerializeField]
    private Rigidbody2D _rigid;
    [Header("Attributes")]
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float accelerationSpeed = 50f;
    [SerializeField]
    private float decelerationSpeed = 30f;
    private Vector2 _moveInput;
    private Vector2 _targetVelocity;
    private Vector2 _aimDirection;
    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
        _playerStatus = GetComponent<Player>();
    }
    
    
    public void Initialise()
    {

    }
    private void Update() {
        GetInput();
    }
    private void FixedUpdate() {
        MovePlayer();
    }
    private void GetInput()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _targetVelocity = _moveInput*_moveSpeed;
        Vector3 mousePosition = KeyboardWeaponInput.GetMousePosition(Camera.main);
        _aimDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
    }
    private void MovePlayer()
    {
        if(_playerStatus.IsStunned || _playerStatus.IsDead)
        {
            _rigid.velocity = Vector2.zero; return;
        }
        if (_moveInput.magnitude > 0.1f)
        {
            _rigid.velocity = Vector2.Lerp(_rigid.velocity, _targetVelocity, accelerationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rigid.velocity = Vector2.Lerp(_rigid.velocity, Vector2.zero, decelerationSpeed * Time.fixedDeltaTime);
        }
    }
}
