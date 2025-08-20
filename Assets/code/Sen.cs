using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sen : MonoBehaviour
{
    public float speed = 2f;                // Tốc độ di chuyển
    public Transform leftLimit, rightLimit; // Giới hạn di chuyển
    public float detectionRange = 2f;      // Khoảng cách để nhân vật có thể tấn công
    public Animator animator;              // Animator để điều khiển hoạt ảnh
    public LayerMask playerLayer;          // Layer của nhân vật chính
    private Rigidbody2D rb;
    private bool movingRight = true;       // Hướng di chuyển
    private bool isDead = false;           // Trạng thái chết
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Kiểm tra nếu các giới hạn di chuyển chưa được gán
        if (leftLimit == null || rightLimit == null)
        {
            Debug.LogError("Giới hạn di chuyển chưa được gán!");
        }

        // Kiểm tra nếu Animator chưa được gán
        if (animator == null)
        {
            Debug.LogError("Animator chưa được gán!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) // Chỉ di chuyển nếu kẻ thù chưa chết
        {
            Move();
            CheckPlayerAttack(); // Kiểm tra tấn công từ nhân vật chính
        }
    }
    void Move()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= rightLimit.position.x)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= leftLimit.position.x)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(movingRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void CheckPlayerAttack()
    {
        // Tìm tất cả đối tượng trong phạm vi detectionRange
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (player != null)
        {
            // Kiểm tra nếu nhân vật chính kích hoạt tấn công
            c1 playerScript = player.GetComponent<c1>();
            if (playerScript != null && playerScript.isAttacking) // Nhân vật tấn công
            {
                // Gọi phương thức chết
                StartCoroutine(Die());
            }
        }
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;                 // Ngăn kẻ thù di chuyển
        rb.velocity = Vector2.zero;    // Dừng di chuyển
        animator.SetTrigger("Dead");   // Kích hoạt hoạt ảnh chết

        // Chờ cho đến khi hoạt ảnh chết kết thúc (giả sử là 1 giây)
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);           // Phá hủy đối tượng
    }

    void OnDrawGizmosSelected()
    {
        // Vẽ phạm vi detectionRange trong Scene để dễ kiểm tra
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
