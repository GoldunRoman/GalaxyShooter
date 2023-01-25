using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed = 1f;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private Player _player;
    private SpawnManager _spawnManager;
    private AudioManager _audioManager;

    private void Start()
    {
        if (!GameObject.Find("Player_1").TryGetComponent(out _player))
            Debug.Log("_player = NULL");

        if (!GameObject.Find("SpawnManager").TryGetComponent(out _spawnManager))
            Debug.Log("_spawnManager = NULL");

        if (!GameObject.Find("AudioManager").TryGetComponent(out _audioManager))
            Debug.LogError("EnemyScriptError! Field _audioManager = NULL!");
    }

    void Update()
    {
        transform.Rotate(_rotSpeed * Time.deltaTime * Vector3.forward); //asteroid rotation
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7) //"Laser" layer
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity); 
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);

            _audioManager.PlayExplosionSound();
        }

        if (other.gameObject.layer == 3) //"Player" layer
        {
            _player.HealthCheck();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            _audioManager.PlayExplosionSound();
        }
    }
    
}
