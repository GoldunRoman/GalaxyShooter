using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bestScoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    private Text _gameoverText;
    private ScenesManager _scenesManager;
    private bool _isRestartMessageActive = false;

    void Start()
    {
        _gameoverText = transform.GetChild(2).gameObject.GetComponent<Text>();
        _scoreText.text = "Score: " + 0;
        _scenesManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isRestartMessageActive == true)
        {
            _scenesManager.ReloadScene();
        }

        else if (Input.GetKeyDown(KeyCode.M) && _isRestartMessageActive == true)
        {
            _scenesManager.LoadMenu();
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();        
    }

    public void UpdateBestScore(int bestScore)
    {
        _bestScoreText.text = "Best: " + bestScore.ToString();
    }

    

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprites[currentLives];
    }

    public void GameOverMessage()
    {
        _isRestartMessageActive = true;
        transform.GetChild(2).gameObject.SetActive(true); //activate "GAME OVER" text                
        StartCoroutine(GameOverFlickerRoutine()); //flashing "GAME OVER" text
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameoverText.text = "GAME OVER!";            
            yield return new WaitForSeconds(0.35f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(0.35f);
        }
    }

    public void RestartMessage()
    {
        if(_isRestartMessageActive)
            transform.GetChild(3).gameObject.SetActive(true); //activate "Restart" annotation
        else
            transform.GetChild(3).gameObject.SetActive(false);
    }
}
