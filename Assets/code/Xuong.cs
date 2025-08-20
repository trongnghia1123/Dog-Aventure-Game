using System.Collections;
using System.Collections.Generic;
using TMPro; // Thêm thư viện TextMeshPro
using UnityEngine;

public class Xuong : MonoBehaviour
{
    public TextMeshProUGUI textSoLuongXuong; // Tham chiếu đến TextMeshPro để hiển thị số lượng xương

    public int soLuongXuong ;           // Biến lưu số lượng xương đã ăn
    public AmThanh amThanh;             // Đối tượng AmThanh để phát âm thanh

    void Start()
    {
        soLuongXuong = 0; // Khởi tạo số lượng xương bằng 0
        // Đảm bảo TextMeshPro không bị null
        if (textSoLuongXuong == null)
        {
            Debug.LogError("TextMeshPro chưa được gắn!");
        }
        // Cập nhật Text ban đầu
        CapNhatTextSoLuongXuong();
    }

    

    // Hàm cập nhật TextMeshPro
    public void CapNhatTextSoLuongXuong()
    {
        if (textSoLuongXuong != null)
        {
            textSoLuongXuong.text = ""+soLuongXuong;
            amThanh.PlayXuongSound(); // Phát âm thanh khi ăn xương
        }
    }
    
}
