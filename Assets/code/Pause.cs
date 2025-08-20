using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel; // Tham chiếu đến panel pause
    public GameObject player;     // Tham chiếu đến nhân vật
    private bool isPaused = false;

    void Update()
    {
        // Kiểm tra khi nhấn phím Escape để bật/tắt menu pause
        if (Input.GetKeyDown(KeyCode.Escape) && player != null)
        {
            TogglePause();
        }

        // Nếu nhân vật chết, hiển thị panel pause
        if (player == null && !pausePanel.activeSelf)
        {
            ShowPausePanel();
        }
    }

    // Hàm để bật/tắt trạng thái pause
    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0; // Dừng game
        }
        else
        {
            Time.timeScale = 1; // Tiếp tục game
        }
    }

    // Hiển thị panel khi nhân vật chết
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0; // Dừng game
    }
}
