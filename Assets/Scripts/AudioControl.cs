using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _clashSound;
    private AudioSource _audioSource;
    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayJumpSound() => _audioSource.PlayOneShot(_jumpSound);

    public void PlayClashSound() => _audioSource.PlayOneShot(_clashSound);
}
