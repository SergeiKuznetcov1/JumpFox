using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    private void Update() {
        bool switchCamera = _playerPos.position.y > 6;
        if (switchCamera)
            transform.position = new Vector3(transform.position.x, 11.0f, transform.position.z);
        else 
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }
}
