using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Health System")]
    public int HEALTH = 3;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    [Header("Speed Settings")]
    [SerializeField]
    private float _currentSpeed; //stores an information about real player`s speed (is it with booster or without?..)
    [SerializeField] 
    private float _defaultSpeed = 10.0f; 
    private float _powerupSpeedMultiplier = 2f;

    [Header("Prefabs Fields")]
    [SerializeField] 
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleshotPrefab;


    [Header("Dammage Effects")]
    [SerializeField]
    private GameObject _rightEngineDmgEffect;
    [SerializeField]
    private GameObject _leftEngineDmgEffect;


    [Header("UI")]
    private int _lastScore = 0;
    private int _bestScore = 0;
    private UIManager _uiManager;

    [Header("Other Settings")]
    [SerializeField] 
    private float _offset = -1.4f;
    [SerializeField] 
    private float _fireRatePlayerOne = 0.3f;
    [SerializeField]
    private float _fireRatePlayerTwo = 0.3f;
    [SerializeField]
    private SpawnManager _spawnManager;
    private float _nextFire = 0f;
    private ScenesManager _scenesManager;

    [Header("Audio Settings")]
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip _laserSound;
    private AudioManager _audioManager;

    //Powerups Flags
    private bool _isTrippleshotActive = false;
    private bool _isSpeedPowerupActive = false;
    private bool _isShieldActive = false;

    void Start()
    {
        _currentSpeed = _defaultSpeed;

        if(!GameObject.Find("SpawnManager").TryGetComponent(out _spawnManager))
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if(!GameObject.Find("Canvas").TryGetComponent(out _uiManager))
        {
            Debug.LogError("The UIManager is NULL!");
        }

        if (!GameObject.Find("ScenesManager").TryGetComponent(out _scenesManager))
        {
            Debug.LogError("The Scenes Manager is NULL");
        }


        if (!GameObject.Find("AudioManager").TryGetComponent(out _audioManager))
        {
            Debug.LogError("_audioManager is NULL!");
        }

        if(!TryGetComponent(out _source))
        {
            Debug.LogError("The _source is NULL!");
        }
        else
        {
            _source.clip = _laserSound;
        }


        if (_scenesManager.isCoopMode == false)
        {
            GameObject.Find("Player_1").GetComponent<Player>().isPlayerOne = true;
            transform.position = Vector3.down * 2.5f;
        }
            
        if(_scenesManager.isCoopMode == true)
        {
            GameObject.Find("Player_1").GetComponent<Player>().isPlayerOne = true;
            GameObject.Find("Player_2").GetComponent<Player>().isPlayerTwo = true;
        }

        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
        _uiManager.UpdateBestScore(_bestScore);
    }

    void Update()
    {
        if(HEALTH <= 0) 
            Destroy(gameObject);

        //PlayerOne controls
        if (isPlayerOne)
        {
            PlayerOneCalculateMovement();

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
            {
                FireLaser(_fireRatePlayerOne);
            }
        }

        //PlayerTwo controls
        if (isPlayerTwo)
        {
            PlayerTwoCalculateMovement();

            if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > _nextFire)
            {
                FireLaser(_fireRatePlayerTwo);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9) //"EnemyLaser" layer
        {
            Destroy(other.gameObject);
            HealthCheck();
        }
    }

    void FireLaser(float fireRate)
    {
        if (_isTrippleshotActive)
        {
            _nextFire = Time.time + fireRate;
            Instantiate(_tripleshotPrefab, new Vector3(transform.position.x, transform.position.y + _offset, transform.position.z), Quaternion.identity);
        }
        else
        {
            _nextFire = Time.time + fireRate;
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + _offset, transform.position.z), Quaternion.identity);
        }

        _source.Play();
        
    }

    void PlayerOneCalculateMovement()
    {
        if (_isSpeedPowerupActive == true)
        {
            _currentSpeed = _defaultSpeed * _powerupSpeedMultiplier;
        }
        else
        {
            _currentSpeed = _defaultSpeed;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(_currentSpeed * Time.deltaTime * direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0f);

        // Teleportation when going off screen:
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void PlayerTwoCalculateMovement()
    {
        if (_isSpeedPowerupActive == true)
        {
            _currentSpeed = _defaultSpeed * _powerupSpeedMultiplier;
        }
        else
        {
            _currentSpeed = _defaultSpeed;
        }

        float horizontalInput = Input.GetAxis("HorizontalAlt");
        float verticalInput = Input.GetAxis("VerticalAlt");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(_currentSpeed * Time.deltaTime * direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0f);

        // Teleportation on the top when going off a screen:
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void HealthCheck() 
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            return; 
        }

        HEALTH--;
        OnHealthEquals(); //if statements
    }

    private void OnHealthEquals()
    {
        if (HEALTH <= 2)
        {
            _rightEngineDmgEffect.SetActive(true);
        }

        if (HEALTH <= 1)
        {
            _leftEngineDmgEffect.SetActive(true);
        }

        _uiManager.UpdateLives(HEALTH);

        if (HEALTH < 1)
        {
            _spawnManager.StopSpawning();
            _uiManager.UpdateBestScore(_bestScore);
            _uiManager.GameOverMessage();
            _uiManager.RestartMessage();
            _audioManager.PlayExplosionSound();
            Destroy(this.gameObject);

        }
    }


    //Powerups
    public void ActivateTripleshot()
    {
        _isTrippleshotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    public void ActivateSpeedBoost()
    {
        _isSpeedPowerupActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }


    //POWERUPS_CORUTINES
    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTrippleshotActive = false;
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(6f);
        _isSpeedPowerupActive = false;
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(8f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        _isShieldActive = false;
    }

    //UI
    public void AddScore(int points)
    {
        _lastScore += points;

        if(_lastScore > _bestScore)
        {
            PlayerPrefs.SetInt("BestScore", _lastScore);
            _bestScore = PlayerPrefs.GetInt("BestScore", 0);
        }

        
        _uiManager.UpdateScore(_lastScore);
    }

    
}
