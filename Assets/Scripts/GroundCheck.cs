using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool Grounded { get; private set; } = false;
    private Rigidbody2D _playerRB;
    private PlayerControl _playerControl;
    private void Awake() {
        _playerRB = GetComponentInParent<Rigidbody2D>();
        _playerControl = GetComponentInParent<PlayerControl>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 6) 
            Grounded = true;
            _playerRB.velocity = Vector2.zero;
            _playerControl.CanJump = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 6) 
            Grounded = false;
            if (!_playerControl.CanJump) 
                _playerRB.velocity = _playerControl.VelocityDuringJump;
    }
}
