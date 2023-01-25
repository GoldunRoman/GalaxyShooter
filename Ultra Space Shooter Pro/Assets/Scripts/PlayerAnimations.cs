using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private Player _player;

    private void Start()
    {
        if (!TryGetComponent(out _animator))
            Debug.LogError("NULL REFERENCE EXCEPTION! THE ANIMATOR IS NULL");

        if (!TryGetComponent(out _player))
            Debug.LogError("NULL REFERENCE EXCEPTION! THE PLAYER IS NULL");
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A) && _player.isPlayerOne == true) || (Input.GetKeyDown(KeyCode.LeftArrow) && _player.isPlayerTwo))
        {
            _animator.SetBool("Turn_Left", true);
            _animator.SetBool("Turn_Right", false);
        }


        else if ((Input.GetKeyUp(KeyCode.A) && _player.isPlayerOne == true) || (Input.GetKeyUp(KeyCode.LeftArrow) && _player.isPlayerTwo))
        {
            _animator.SetBool("Turn_Right", false);
            _animator.SetBool("Turn_Left", false);
        }

        if ((Input.GetKeyDown(KeyCode.D) && _player.isPlayerOne == true) || (Input.GetKeyDown(KeyCode.RightArrow) && _player.isPlayerTwo))
        {
            _animator.SetBool("Turn_Right", true);
            _animator.SetBool("Turn_Left", false);
        }

        else if ((Input.GetKeyUp(KeyCode.D) && _player.isPlayerOne == true) || (Input.GetKeyUp(KeyCode.RightArrow) && _player.isPlayerTwo))
        {
            _animator.SetBool("Turn_Right", false);
            _animator.SetBool("Turn_Left", false);
        }
    }
}
