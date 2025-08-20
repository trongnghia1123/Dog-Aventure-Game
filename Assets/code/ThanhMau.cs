using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThanhMau : MonoBehaviour
{
    public Image Thanhmau;         // Hình ảnh thanh máu
    public float mauToiDa = 100f;  // Máu tối đa của nhân vật
    public float mauHienTai;      // Máu hiện tại của nhân vật
    public c1 nhanVat;             // Script c1 gắn vào nhân vật
    void Start()
    {
        // Đặt máu hiện tại bằng máu tối đa khi bắt đầu
        mauHienTai = mauToiDa;
        CapNhatThanhMau();
    }

    // Hàm giảm máu mỗi khi bị trúng đạn
    public void TruMau(float luongMau)
    {
        mauHienTai -= luongMau;
        mauHienTai = Mathf.Clamp(mauHienTai, 0, mauToiDa); // Đảm bảo máu không nhỏ hơn 0
        CapNhatThanhMau();

        if (mauHienTai <= 0)
        {
            NhanVatChet(); // Gọi hàm xử lý khi nhân vật chết
        }
    }

    // Hàm cập nhật thanh máu
    public void CapNhatThanhMau()
    {
        Thanhmau.fillAmount = mauHienTai / mauToiDa;
        if (mauHienTai <= 0)
        {
            NhanVatChet();
        }
    }

    // Hàm xử lý khi nhân vật chết
    private void NhanVatChet()
    {
        Debug.Log("Nhân vật đã chết!");
        
        // Tìm script c1 gắn vào nhân vật để gọi hàm NhanVatChet
        if (nhanVat != null)
        {
            nhanVat.NhanVatChet(); // Gọi hàm xử lý chết từ c1
        }
        else
        {
            Debug.LogWarning("Script c1 không được tìm thấy trên nhân vật!");
        }
        gameObject.SetActive(false); // Ẩn nhân vật
    }
    // Phát hiện va chạm với đối tượng "tim"
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tim"))
        {
            mauHienTai = mauToiDa; // Làm đầy lại thanh máu
            CapNhatThanhMau(); // Cập nhật thanh máu
            Destroy(collision.gameObject); // Xóa đối tượng "tim" sau khi ăn
        }
    }
    */
}
