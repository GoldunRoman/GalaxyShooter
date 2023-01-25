using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSingleMode()
    {
        SceneManager.LoadScene("Single_Player"); 
    }

    public void LoadCooperativeMode()
    {
        SceneManager.LoadScene("Coop_Mode");
    }
}
