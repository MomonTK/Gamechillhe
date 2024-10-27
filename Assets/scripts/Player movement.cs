using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //tạo nút speed
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Rigidbody2D body;

    [Header("Dash Setting")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing;
    bool canDash;

    Vector2 moveDirection;
    Vector2 mousePosition;

    //2
    private Animator anim;

    //3
    private BoxCollider2D boxCollider;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;


    //dùng Awake để hàm được bật trước Start (###########)
    private void Awake()
    {
        //2
        anim = GetComponent<Animator>();
        //1
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

       
    }

    private void Start()
    {
        canDash = true;
    }


    //hàm Update được phản hồi liên tục (chạy liên tục) vd: khi bấm nút di chuyển, hàm sẽ biết và cho player move
    private void Update()
    {
        //2
        //Everytime we dashing, not one the code below to get call 
        if(isDashing)
        {
            return;
        }

        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");

        //đổi hướng nhân vật khi nhân vật quay trái phải
        if (HorizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(10, 10, -10);
        }
        if (HorizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-10, 10, 10);
        }

        if (VerticalInput > 0.01f)
        {
            transform.localScale = new Vector3(10, 10, 10);
        }
        if (VerticalInput < -0.01f)
        {
            transform.localScale = new Vector3(10, -10, 10);
        }


        /*Vector2 vi player di chuyển theo 2 Axis X, Y
        Input.Axis("Horizontal") là câu lệnh để tạo nút điều khiển qua Horizontal, sẽ trả về giá trị thực -1...1 nếu key right or key left được bấm
        new Vector2 sẽ tạo ra vector2 mới mỗi lần lệnh được thực hiện (###########)
        tạo nút speed trong giao diện Unity và speed giúp thay đổi tốc độ của player
        body.velocity.y sẽ giữ nguyên giá trị của trục Y khi câu lệnh được thực thi thì thằng còn lại được gọi*/
        //câu lệnh gán nút A để player tiến tới -X, nút D để player tiến tới +X
        body.velocity = new Vector2(Input.GetAxisRaw("Vertical") * speed, body.velocity.y);
        body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, body.velocity.x);


        /*&& Grouded là điều kiện thực hiện, chỉ thực hiện nút Space được khi Player trên Ground*/
        //câu này giúp gán chức năng cho nút Space, Getkey chứa tất cả các nút để gọi ra
        //1
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    

        //2
        /*ta đã thiệt lập trong ứng dụng phần Condition từ Idle chuyển trạng thái qua Run sẽ bằng True
        Nghĩa là ta gán 2 giá trị bằng nhau vào Run thì Run sẽ được thực thi
        Và khi ta không bấm nút di chuyển thì Axis của Player bằng 0 */
        anim.SetBool("Run", HorizontalInput != 0 || VerticalInput != 0);

       
        /*mà trên code, ta cho Run bằng tọa độ hiện tại khác 0, nghĩa là 0 != 0 
        //mà như vậy là False, nên Run sẽ không được thực thi, và ngược lại*/

        /*anim.SetBool("grounded", isGrounded());*/

        print(onWall());

        moveDirection = new Vector2(HorizontalInput, VerticalInput).normalized;

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
       body.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);   
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true; 
    }

    /* public bool canAttack()
     {
         return Input.GetAxis("Horizontal") == 0 && isGrounded();
     }*/

    /*private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f,groundLayer);
            return raycastHit.collider != null;
    }*/

    private bool onWall()
    { 
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
