using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] 
    private float _speed = 0.1f;
    [SerializeField]
    private BoxCollider2D _boxCollider;


    [Header("Shooting Settings")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _laserOffset = 1.5f;

    private Player _player;
    private Animator _animator;
    private AudioManager _audioManager;

    private void Start()
    {
        if (!GameObject.Find("Player_1").TryGetComponent(out _player))
            Debug.LogError("EnemyScriptError! Field _player1 = NULL!");

        if(!TryGetComponent(out _animator))
            Debug.Log("The Animator is NULL!");

        if (!GameObject.Find("AudioManager").TryGetComponent(out _audioManager))
            Debug.LogError("EnemyScriptError! Field _audioManager = NULL!");


        StartCoroutine(EnemyLaserRandomRoutine());

    }
    void Update()
    {
        CalculateMovement(_speed);

        if (gameObject.transform.position.y < -6f)
        {
            float spawnX = Random.Range(-10, 10);
            float spawnY = 12f;
            transform.position = new Vector3(spawnX, spawnY, 0f);
        }

    }

    void CalculateMovement(float speed)
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 7) //"Laser" layer
        {
            Destroy(other.gameObject);
            OnDeath();
            _player.AddScore(10);
            
        }

        if(other.gameObject.layer == 3) //"Player" layer
        {
            if(other.transform.TryGetComponent<Player>(out var player))
            {
                OnDeath();
                player.HealthCheck();
            }
        } 
    }

    private void OnDeath()
    {
        _boxCollider.enabled = false;
        _animator.SetTrigger("OnEnemyDeath");
        SpeedController(1.3f);
        _audioManager.PlayExplosionSound();
    }

    public void SpeedController(float speed)
    {
        _speed = speed;
    }

    IEnumerator EnemyLaserRandomRoutine()
    {
        while(this.gameObject != null)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y - _laserOffset, transform.position.z), Quaternion.identity);
        }
    }
}
