using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private PlayerControl _playerControl;
    private void Awake() {
        _playerControl = GetComponent<PlayerControl>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _anim.SetFloat("xVelocity", _rb.velocity.x);
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("isGrounded", _groundCheck.Grounded);
        _anim.SetBool("canMove", _playerControl.CanMove);
    }
}
