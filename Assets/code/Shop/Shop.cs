using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public void Exit()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map_level");
    }
}
