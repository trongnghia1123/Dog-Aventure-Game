using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phao : MonoBehaviour
{
    public GameObject bullet;     // Prefab viên đạn
    public Transform firePoint;   // Điểm xuất phát của viên đạn
    private float timer;          // Bộ đếm thời gian
    public float bulletSpeed = 5f; // Tốc độ viên đạn 
    public float fireInterval = 2f; // Khoảng thời gian giữa các lần bắn

    void Start()
    {
        timer = fireInterval; // Khởi tạo bộ đếm thời gian
    }

    void Update()
    {
        // Đếm ngược thời gian để bắn
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Fire(); // Bắn viên đạn
            timer = fireInterval; // Đặt lại bộ đếm
        }
    }

    // Hàm bắn đạn
    void Fire()
    {
        if (bullet != null && firePoint != null)
        {
            GameObject spawnedBullet = Instantiate(bullet, firePoint.position, Quaternion.identity); // Tạo viên đạn
            Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.right * bulletSpeed * -1; // Đặt vận tốc viên đạn theo trục x
            }
        }
    }
}
