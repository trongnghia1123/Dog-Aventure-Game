using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dan : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Hủy viên đạn khi va chạm với bất kỳ Collider nào
        Destroy(gameObject);
    }
}
