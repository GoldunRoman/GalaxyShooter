using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    //0 - tripleshot    //1 - speed   //2 - shield
    private int powerupID;
    private AudioManager _audioManager;

    private void Start()
    {
        if (!GameObject.Find("AudioManager").TryGetComponent(out _audioManager))
        {
            Debug.LogError("_audioManager is NULL!");
        }
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // If leave the screen destroy
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }

        // Moving down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 3) //Player layer
        {
            Destroy(this.gameObject);

            if(other.TryGetComponent<Player>(out var player))
            {
                switch (powerupID)
                {
                    case 0:
                        player.ActivateTripleshot();                       
                        break;

                    case 1:
                        player.ActivateSpeedBoost();
                        break;

                    case 2:
                        player.ActivateShield();
                        break;

                    default:
                        Debug.Log("Default Value");
                        break;
                }
                _audioManager.PlayPowerupSound();
            }
        }
    }
}
