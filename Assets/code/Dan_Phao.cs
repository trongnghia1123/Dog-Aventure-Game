using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dan_Phao : MonoBehaviour
{
    public int damage = 10;               // Sát thương của viên đạn
    public float maxDistance = 10f;      // Khoảng cách tối đa viên đạn có thể di chuyển
    private Vector3 startPosition;       // Vị trí ban đầu của viên đạn

    void Start()
    {
        startPosition = transform.position; // Lưu vị trí khi viên đạn được bắn
    }

    void Update()
    {
        // Kiểm tra quãng đường di chuyển
        float traveledDistance = Vector3.Distance(startPosition, transform.position);
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject); // Hủy viên đạn nếu vượt quá khoảng cách tối đa
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Hủy viên đạn khi va chạm với bất kỳ Collider nào
        Destroy(gameObject);
    }
/*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_1")) // Kiểm tra va chạm với nhân vật
        {
            Debug.Log("Viên đạn đã trúng nhân vật!");
            ThanhMau thanhMau = collision.GetComponent<ThanhMau>();
            if (thanhMau != null)
            {
                thanhMau.TruMau(damage); // Trừ máu nhân vật
            }

            Destroy(gameObject); // Xóa viên đạn sau khi va chạm
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            Destroy(gameObject); // Xóa viên đạn nếu va chạm với mặt đất
        }
    }*/
}
