using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public bool isCoopMode;
    [SerializeField]
    private GameObject _pauseMenu;
    private Animator _pauseAnimator;
    [SerializeField]
    private Player _player;

    private void Start()
    {
        if (!GameObject.Find("Pause_Menu_Panel").TryGetComponent(out _pauseAnimator))
            Debug.LogError("NULL reference exception! The Animator is NULL!");

        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //curr game scene
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Coop_Menu");
    }

    private void Pause()
    {
        _pauseMenu.SetActive(true);
        _pauseAnimator.SetBool("isPaused", true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
