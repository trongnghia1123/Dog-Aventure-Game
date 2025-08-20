using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimCuong : MonoBehaviour
{
    private Win winScript;
    public AmThanh amThanh; // Đối tượng AmThanh để phát âm thanh
    void Start()
    {
        // Tìm script Win trong game
        winScript = FindObjectOfType<Win>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_1")) // Kiểm tra va chạm với nhân vật
        {
            if (winScript != null)
            {
                winScript.TangKimCuong(); // Tăng số kim cương đã thu thập
                
            }
            amThanh.PlayKimCuongSound(); // Phát âm thanh khi ăn kim cương
            
            Destroy(gameObject); // Xóa kim cương sau khi thu thập
        }
    }
}
