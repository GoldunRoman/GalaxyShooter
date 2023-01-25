using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip _explosionClip, _powerupClip;

    private void Start()
    {
        if (!TryGetComponent(out _source))
        {
            Debug.LogError("_source is NULL!");
        }
    }

    public void PlayExplosionSound()
    {
        _source.clip = _explosionClip;
        _source.Play();
    }

    public void PlayPowerupSound()
    {
        _source.clip = _powerupClip;
        _source.Play();
    }



}
