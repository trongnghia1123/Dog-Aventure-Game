using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaibay : MonoBehaviour
{
    public GameObject bullet;     // Prefab viên đạn
    public Transform firePoint;   // Điểm xuất phát của viên đạn
    private float timer;          // Bộ đếm thời gian
    private GameObject player;    // Đối tượng nhân vật
    public float bulletSpeed = 5f; // Tốc độ viên đạn

    void Start()
    {
        // Tìm nhân vật có tag "Player_1"
        player = GameObject.FindGameObjectWithTag("Player_1");
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Bắn đạn sau mỗi 2 giây
        if (timer >= 2)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
{
    if (player == null) return; // Đảm bảo nhân vật tồn tại

    // Tạo viên đạn tại firePoint
    GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);

    // Gán Tag "Dan" cho viên đạn
    newBullet.tag = "Dan";

    // Tính hướng tới nhân vật
    Vector3 direction = (player.transform.position - firePoint.position).normalized;

    // Gắn vận tốc cho viên đạn
    Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
    if (bulletRb != null)
    {
        bulletRb.velocity = direction * bulletSpeed;
    }

    // Tùy chỉnh góc quay của viên đạn để hướng về phía nhân vật
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    newBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Chỉnh góc quay để căn chỉnh với hướng viên đạn
}

}
