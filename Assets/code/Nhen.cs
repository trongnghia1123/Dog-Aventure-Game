using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nhen : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftLimit, rightLimit;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool isDead = false; // Trạng thái chết
    public Animator animator;   // Tham chiếu đến Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead) // Chỉ di chuyển nếu kẻ thù chưa chết
        {
            Move();
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

    // Xử lý khi bị nhân vật nhảy lên
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player_1"))
        {
            // Kiểm tra nếu nhân vật nhảy từ trên xuống
            if (collision.relativeVelocity.y <= 0 && collision.transform.position.y > transform.position.y)
            {
                // Đẩy nhân vật lên
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6);

                // Kích hoạt trạng thái chết
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        isDead = true;                 // Ngăn kẻ thù di chuyển
        rb.velocity = Vector2.zero;    // Dừng di chuyển
        animator.SetTrigger("Dead");   // Kích hoạt animation chết

        // Chờ cho đến khi animation kết thúc (thời gian của animation là 1 giây)
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);           // Phá hủy đối tượng
    }
}
