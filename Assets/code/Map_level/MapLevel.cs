using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevel : MonoBehaviour
{
    public void Level_1()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Van_1");
    }

    public void Level_2()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Van_2");
    }
    public void Level_3()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Van_3");
    }
    public void Level_4()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Van_4");
    }
    public void Level_5()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Van_5");
    }
    public void Shop()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }
        public void Exit()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
    }
}
