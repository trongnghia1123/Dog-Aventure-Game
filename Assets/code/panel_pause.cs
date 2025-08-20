using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class panel_pause : MonoBehaviour
{
    public GameObject pausePanel; // Tham chiếu đến panel pause

    public void LoadMainMenu()
    {
        // Chuyển đến Main Menu
        Time.timeScale = 1; // Reset thời gian để tránh lỗi
        SceneManager.LoadScene("mainMenu");
    }

    public void MapLevel()
    {
        // Chuyển đến Main Menu
        Time.timeScale = 1; // Reset thời gian để tránh lỗi
        SceneManager.LoadScene("Map_level");
    }

    public void RestartLevel()
    {
        // Load lại màn chơi hiện tại
        Time.timeScale = 1; // Reset thời gian để tránh lỗi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClosePausePanel()
    {
        // Tắt panel pause và tiếp tục game
        pausePanel.SetActive(false);
        Time.timeScale = 1; // Tiếp tục thời gian game
    }
}
