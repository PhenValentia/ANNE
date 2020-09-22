using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour // Entirely by NDS8
{
    // Upon clicking play, load the introduction scene.
    public void PlayGame()
    {
        SceneManager.LoadScene("Introduction");
    }

    // Upon clicking quit, quit the game.
    public void QuitGame()
    {
        Debug.Log("Game has Quit!");
        Application.Quit();
    }
}
