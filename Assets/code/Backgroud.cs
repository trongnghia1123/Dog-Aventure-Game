using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform mainCamera;     // Camera chính
    public Transform midBackground; // Background chính giữa
    public Transform farBackground; // Background xa hơn
    public float leng;              // Chiều dài của một đoạn background
    public float offset ;     // Giá trị bù trừ để cập nhật trước khi camera chạm đến ranh giới

    void Update()
    {
        // Khi camera gần chạm ranh giới bên phải
        if (mainCamera.position.x > midBackground.position.x + (leng / 2) - offset)
        {
            UpdateBackgroundPosition(Vector3.right);
        }
        // Khi camera gần chạm ranh giới bên trái
        else if (mainCamera.position.x < midBackground.position.x - (leng / 2) + offset)
        {
            UpdateBackgroundPosition(Vector3.left);
        }
    }

    void UpdateBackgroundPosition(Vector3 direction)
    {
        // Di chuyển background xa nhất sang vị trí mới
        farBackground.position = midBackground.position + direction * leng;

        // Hoán đổi vị trí để duy trì trật tự
        Transform temp = midBackground;
        midBackground = farBackground;
        farBackground = temp;
    }
}
