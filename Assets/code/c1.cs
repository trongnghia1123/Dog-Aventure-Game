using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class c1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public int Tocdo ;  // Tốc độ bình thường
    public float diChuyenTraiPhai;
    public bool daoChieu = true;
    // Hoạt ảnh 'animation'
    public Animator hoatanh;

    // Nhảy 'double jump'
    public bool isgrounded;          // Kiểm tra xem có trên mặt đất không
    public bool canDoubleJump;       // Có thể nhảy 2 lần
    public Transform groundCheck;    // Kiểm tra xem có trên mặt đất không
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public float Lucnhay;

    // Kiểm tra vùng nước
    public bool isInWater;
    public Transform waterCheck;     // Vị trí kiểm tra tiếp xúc với nước
    public LayerMask waterLayer;     // Layer đại diện cho nước
    public float waterCheckRadius;   // Bán kính kiểm tra vùng nước
    private int tocdoBanDau;         // Lưu tốc độ ban đầu

    public float tocDoDiChuyen;  // Tốc độ di chuyển ngang
    public AmThanh amThanh;      // Đối tượng AmThanh để phát âm thanh

    private string currentZone = ""; // Tên vùng hiện tại (Water, Ground, Sand, Brick)
    public ThanhMau thanhMau;       // Đối tượng ThanhMau để cập nhật thanh máu
    //public float mauHienTai;        // Máu hiện tại
    //public float mauToiDa;          // Máu tối đa
    public Xuong anXuong;           // Đối tượng Xuong để cập nhật số lượng xương

    private bool isDead = false;    // Trạng thái chết

    public float attackRange = 2f; // Khoảng cách tấn công
    public LayerMask enemyLayers;    // Layer của đối tượng địch

    //----------------------------------------------
    public bool isAttacking = false; // Trạng thái tấn công
    private float moveDirection = 0f; // Hướng di chuyển (0, 1, hoặc -1)

    public Win winScript; // Đối tượng Win để gọi hàm khi thắng
    void Start()
    {
        tocdoBanDau = Tocdo;         // Lưu tốc độ ban đầu


        if (amThanh == null)
        {
            amThanh = FindObjectOfType<AmThanh>(); // Tìm script âm thanh trong scene
        }

    }
    

    //----------------------------------------------
    
    void Update()
{
    if (isDead || isAttacking) return; // Không thực hiện nếu nhân vật đã chết hoặc đang tấn công

    // Kiểm tra xem nhân vật có đang tiếp xúc với nước không
    bool isInWater = Physics2D.OverlapCircle(waterCheck.position, waterCheckRadius, waterLayer);

    // Nếu nhân vật trong nước, giảm tốc độ
    if (isInWater)
    {
        Tocdo = 2; // Giảm tốc độ xuống 2
    }
    else
    {
        Tocdo = tocdoBanDau; // Khôi phục tốc độ ban đầu
    }

    // Kiểm tra trạng thái trên mặt đất
    isgrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    // Cập nhật vận tốc để di chuyển
    if (moveDirection != 0)
    {
        rb.velocity = new Vector2(moveDirection * Tocdo, rb.velocity.y);

        // Đổi hướng dựa trên chiều di chuyển
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2); // Quay mặt sang trái
        }
        else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(2, 2, 2); // Quay mặt sang phải
        }

        // Cập nhật hoạt ảnh di chuyển
        hoatanh.SetFloat("dichuyen", Mathf.Abs(moveDirection));
    }
    else
    {
        // Dừng di chuyển
        
        rb.velocity = new Vector2(0, rb.velocity.y);
        hoatanh.SetFloat("dichuyen", 0);
    }

    // Kiểm tra trạng thái rơi
    if (!isgrounded && rb.velocity.y < 0)
    {
        hoatanh.SetBool("Roi", true); // Kích hoạt hoạt ảnh rơi
    }
    else
    {
        hoatanh.SetBool("Roi", false); // Tắt hoạt ảnh rơi
    }

    // Xử lý nhảy
    if (Input.GetButtonDown("Jump"))
    {
        if (isgrounded)
        {
            Jump(); // Nhảy lần đầu
            canDoubleJump = true; // Cho phép nhảy lần 2
        }
        else if (canDoubleJump)
        {
            Jump(); // Nhảy lần thứ 2
            canDoubleJump = false;
        }
    }

    // Kiểm tra vùng và âm thanh theo layer
    float tocDoDiChuyen = Mathf.Abs(rb.velocity.x); // Lấy tốc độ di chuyển ngang
    if (!string.IsNullOrEmpty(currentZone) && tocDoDiChuyen > 0.1f)
    {
        amThanh.PlaySoundByLayer(LayerMask.NameToLayer(currentZone));
    }
    /*else
    {
        amThanh.StopSound(); // Dừng phát âm thanh nếu không di chuyển
    }*/
}
    
    //----------------------------------------------
    // Xử lý va chạm với các đối tượng khác

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Xác định nhân vật đã vào vùng nào
        currentZone = LayerMask.LayerToName(collision.gameObject.layer);
        Debug.Log("Va chạm với: " + collision.gameObject.name);

        if (currentZone == "Bay")
        {
            Debug.Log("Nhân vật va chạm với vùng Bay!");
            thanhMau.TruMau(100); // Trừ 100 máu
            hoatanh.SetTrigger("BiThuong"); // Kích hoạt hoạt ảnh bị thương
            amThanh.PlayHurtSound(); // Phát âm thanh bị thương
        }

        if (collision.CompareTag("Xuong"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Va chạm với xương!");
            amThanh.PlayXuongSound();
            Debug.Log("Âm thanh ăn xương đã phát!");
            anXuong.soLuongXuong ++;
            anXuong.CapNhatTextSoLuongXuong();
            
        }

        if (collision.CompareTag("Kimcuong"))
        {
            Destroy(collision.gameObject);

            winScript.TangKimCuong(); // Tăng số kim cương đã thu thập

            Debug.Log("Va chạm với kim cương!");
            amThanh.PlayKimCuongSound();
            Debug.Log("Âm thanh ăn kim cương đã phát!");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Khi nhân vật rời vùng, xóa vùng hiện tại và dừng âm thanh
        if (LayerMask.LayerToName(collision.gameObject.layer) == currentZone)
        {
            currentZone = "";
            amThanh.StopSound();
        }
    }
    // Trừ máu khi va chạm với đối tượng khác
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dan")) // Nếu va chạm với viên đạn
        {
            thanhMau.TruMau(20); // Trừ 20 máu
            hoatanh.SetTrigger("BiThuong"); // Kích hoạt hoạt ảnh bị thương
            amThanh.PlayHurtSound(); // Phát âm thanh khi bị thương
            Destroy(collision.gameObject); // Xóa viên đạn
        }

        if (collision.gameObject.CompareTag("Dan_phao")) // Nếu va chạm với viên đạn
        {
            thanhMau.TruMau(10); // Trừ 10 máu
            hoatanh.SetTrigger("BiThuong"); // Kích hoạt hoạt ảnh bị thương
            amThanh.PlayHurtSound(); // Phát âm thanh khi bị thương
            Destroy(collision.gameObject); // Xóa viên đạn
        }

        if (collision.gameObject.CompareTag("Sen")) // Nếu va chạm với sên
        {

            thanhMau.TruMau(20); // Trừ 20 máu
            hoatanh.SetTrigger("BiThuong"); // Kích hoạt hoạt ảnh bị thương
            amThanh.PlayHurtSound(); // Phát âm thanh khi bị thương
        }

        if (collision.gameObject.CompareTag("Nhen")) // Nếu va chạm với nhện
        {
            // Kiểm tra nếu groundCheck có va chạm với nhện
            Collider2D groundCheckCollider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("Nhen"));

            if (groundCheckCollider != null) // Nếu groundCheck va chạm với nhện
            {
                // Đẩy nhân vật lên một lực nhỏ
                rb.velocity = new Vector2(rb.velocity.x, Lucnhay);

                // Xóa nhện bằng cách gọi Die()
                var nhen = collision.gameObject.GetComponent<Nhen>();
                if (nhen != null)
                {
                    nhen.StartCoroutine("Die");
                }
            }
            else // Nếu nhân vật va chạm trực tiếp với nhện mà không qua groundCheck
            {
                // Trừ máu nhân vật
                thanhMau.TruMau(20);
                hoatanh.SetTrigger("BiThuong"); // Kích hoạt hoạt ảnh bị thương
                amThanh.PlayHurtSound(); // Phát âm thanh bị thương
            }
        }


        if (collision.gameObject.CompareTag("Tim"))
        {
            thanhMau.mauHienTai = thanhMau.mauToiDa; // Làm đầy lại thanh máu
            thanhMau.CapNhatThanhMau(); // Cập nhật thanh máu
            Destroy(collision.gameObject); // Xóa đối tượng "Tim"
            amThanh.PlayHealSound(); // Phát âm thanh hồi máu
        }

    }

    // Hàm xử lý khi nhân vật chết
    public void NhanVatChet()
    {
        isDead = true; // Đặt trạng thái chết
        if (hoatanh != null)
        {
            hoatanh.SetTrigger("Chet"); // Kích hoạt hoạt ảnh chết
            Debug.Log("Hoạt ảnh chết đã kích hoạt!");
            amThanh.PlayDeathSound(); // Phát âm thanh chết
        }
        else
        {
            Debug.LogWarning("Animator không được gắn!");
        }
        StartCoroutine(DestroyAfterAnimation()); // Chờ hoạt ảnh chết trước khi xóa nhân vật
    }


    // Coroutine chờ xóa nhân vật sau khi hoạt ảnh kết thúc
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1.5f); // Thời gian chờ bằng với hoạt ảnh chết
        Destroy(gameObject); // Xóa nhân vật
    }

    public void MoveLeft()
    {
        moveDirection = -1f;
        transform.localScale = new Vector3(-2, 2, 2); // Đổi hướng nhân vật
    }

    // Di chuyển sang phải (liên kết với nút phải)
    public void MoveRight()
    {
        moveDirection = 1f;
        transform.localScale = new Vector3(2, 2, 2); // Đổi hướng nhân vật
    }

    // Dừng di chuyển (liên kết với sự kiện thả nút di chuyển)
    public void StopMoving()
    {
        moveDirection = 0f;
    }

    // Nhảy (liên kết với nút nhảy)
    public void Jump()
    {
        if (isgrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, Lucnhay);
            amThanh.PlayJumpSound(); // Phát âm thanh nhảy
        }
    }

    // Tấn công (liên kết với nút tấn công)
    public void Attack()
    {
        if (!isAttacking)
    {
        isAttacking = true;
        hoatanh.SetTrigger("TanCong"); // Kích hoạt hoạt ảnh tấn công
        amThanh.PlayAttackSound(); // Phát âm thanh tấn công
        StartCoroutine(ResetAttack()); // Reset trạng thái sau khi tấn công

        // Gửi thông báo tấn công
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Sen>().TakeDamage();
        }
    }
    }

    // Đặt lại trạng thái tấn công sau một khoảng thời gian
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Thời gian hoạt ảnh tấn công
        isAttacking = false;
    }


    
}
