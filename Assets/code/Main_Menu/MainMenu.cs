using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map_level");
    }
    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
        Debug.Log("Da thoat game");
    }
}
