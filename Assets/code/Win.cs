using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.SearchService; 
// Dùng TextMeshPro

public class Win : MonoBehaviour
{
    public GameObject panelWin;            // Panel Win để hiển thị khi nhân vật thắng
    public TextMeshProUGUI textTongXuong;  // Text để hiển thị tổng số lượng xương
    public GameObject[] diamonds;         // Mảng chứa các hình ảnh kim cương (3 phần tử)
    public Xuong xuongScript;             // Script Xuong để lấy thông tin số lượng xương
    private int soLuongKimCuong;          // Số kim cương đã thu thập

    public string nextSceneName; 
    void Start()
    {
        if (panelWin != null)
            panelWin.SetActive(false); // Ẩn panel Win khi bắt đầu
    }

    // Hàm gọi khi nhân vật va chạm với đối tượng Win
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_1")) // Kiểm tra va chạm với nhân vật
        {
            ShowWinPanel(); // Hiển thị panel thắng
        }
    }

    // Hiển thị panel Win
    public void ShowWinPanel()
    {
        if (panelWin != null)
        {
            panelWin.SetActive(true); // Hiển thị panel Win

            // Hiển thị tổng số lượng xương
            if (xuongScript != null)
            {
                int tongSoXuong = xuongScript.soLuongXuong; // Lấy số lượng xương
                textTongXuong.text = " " + tongSoXuong; // Hiển thị
            }
            else
            {
                Debug.LogError("Chưa gắn script Xuong vào Win!"); // Thông báo lỗi
            }

            // Hiển thị số kim cương tương ứng
            for (int i = 0; i < diamonds.Length; i++)
            {
                diamonds[i].SetActive(i < soLuongKimCuong); // Hiển thị hoặc ẩn hình kim cương
            }
        }
    }

    // Hàm tăng số lượng kim cương (gọi từ các đối tượng kim cương trong game)
    public void TangKimCuong()
    {
        soLuongKimCuong++;
        if (soLuongKimCuong > 3) soLuongKimCuong = 3; // Giới hạn tối đa 3 kim cương
    }
    public void Nextlevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Tên scene tiếp theo chưa được gán!");
        }
    }
}
