using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; 
    [SerializeField] private float _jumpForceY;
    [SerializeField] private float _jumpForceX;
    [SerializeField] private float _jumpGrowSpeed;
    [SerializeField] private float _jumpMax;
    [SerializeField] private float _jumpMin;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PhysicsMaterial2D _standart;
    [SerializeField] private PhysicsMaterial2D _bounce;
    [SerializeField] private AudioControl _audioControl;
    public Vector2 VelocityDuringJump { get; private set; } 
    private bool _grounded;
    private Rigidbody2D _rb;
    private float _inputX;
    private float _initScaleX;
    public bool CanJump { get; set;} = true;
    private BoxCollider2D _boxCol;
    private GroundCheck _groundCheck;
    public bool CanMove { get; private set; }
    private void Awake() {
        _boxCol = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _initScaleX = transform.localScale.x;
    }    

    private void FixedUpdate() {
        if (_groundCheck.Grounded && CanMove)
            _rb.velocity = new Vector2(_inputX * _moveSpeed, _rb.velocity.y);
    }
    private void Update() {
        
        _inputX = Input.GetAxisRaw("Horizontal");
        ChangeLocalScale();
        ChangeMaterial();
        if (_groundCheck.Grounded) 
            PerformeJump();
        if (_rb.velocity.y < 0.0f) {
            CanMove = true;
            VelocityDuringJump = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _groundCheck.Grounded) {
            CanMove = false;
            _rb.velocity = Vector2.zero;
        }
        // else if (Input.GetKeyUp(KeyCode.Space))
        //      CanJump = true;
    }

    private void PerformeJump() {
        if (CanJump && _groundCheck.Grounded) {
            if (Input.GetKey(KeyCode.Space) ) {
                _jumpForceY += Time.deltaTime * _jumpGrowSpeed;
                if (_jumpForceY >= _jumpMax)
                    Jump();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
                Jump();
        }
    }

    private void Jump() {
        if (_jumpForceY < _jumpMin)
            _jumpForceY = _jumpMin;
        _rb.velocity = new Vector2(_jumpForceX * _jumpForceY / 2, _jumpForceY);
        VelocityDuringJump = _rb.velocity;
        _audioControl.PlayJumpSound();
        _jumpForceY = 0.0f;
        CanJump = false;
    }

    private void ChangeMaterial() {
        if (_groundCheck.Grounded)
            _boxCol.sharedMaterial = _standart;
        else if (!_groundCheck.Grounded)
            _boxCol.sharedMaterial = _bounce;
    }

    private void ChangeLocalScale() {
        if (!_groundCheck.Grounded || !CanMove) 
            return;
        if (Input.GetAxisRaw("Horizontal") > 0.0f) {
            transform.localScale = new Vector3(_initScaleX, transform.localScale.y, transform.localScale.z);
            _jumpForceX = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0.0f) {
            transform.localScale = new Vector3(-_initScaleX, transform.localScale.y, transform.localScale.z);
            _jumpForceX = -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!_groundCheck.Grounded)
            _audioControl.PlayClashSound();
    }
}
