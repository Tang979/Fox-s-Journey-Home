using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private  CapsuleCollider2D standCollider;
    [SerializeField] private CapsuleCollider2D crouchCollider;
    [SerializeField] private bool isCrouching;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool isGrounded;
    private bool isFlipRight = true;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction crouchAction;
    private Animator animator;
    private Vector2 moveValue;
    
    private float baseGravity = 2f;
    private float maxFallSpedd = 18f;
    private float fallSpeedMultiplier = 2f;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"]; // Lấy hành động di chuyển
        // Lấy Input Action từ Input System
        crouchAction = playerInput.actions["Crouch"];
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(isDead)
        {
            return;
        }
        CheckDown();
        // Đọc giá trị đã được packed
        moveValue = moveAction.ReadValue<Vector2>();
        // Điều khiển hướng nhân vật
        if(moveValue.x > 0 && !isFlipRight)
        {
            Flip();
        }
        else if(moveValue.x < 0 && isFlipRight)
        {
            Flip();
        }
        // Điều khiển animation run
        animator.SetBool("isRunning", moveValue.x != 0);
        
        animator.SetFloat("yvelocity", rb.linearVelocity.y);
        animator.SetBool("isJumping", !isGrounded);

        // Kiểm tra nếu đang giữ phím S (crouchAction được kích hoạt)
        isCrouching = crouchAction.ReadValue<float>() > 0;
        if(isCrouching){
            standCollider.enabled = false;
            crouchCollider.enabled = true;
            moveValue.x = 0;
        }
        else
        {
            standCollider.enabled = true;
            crouchCollider.enabled = false;
        }
        // Cập nhật animation
        animator.SetBool("isCrouching", isCrouching);

        // Điều khiển nhân vật dựa vào input
        transform.Translate(moveValue * Time.deltaTime * moveSpeed);
        Gravity();
    }
    public void Die()
    {
        isDead = true;
        rb.linearVelocityY = jumpForce/2;
        standCollider.enabled = false;
        crouchCollider.enabled = false;
        animator.SetTrigger("isDead");
        Destroy(gameObject, 2f);
        GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>().Follow = null;
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        isFlipRight = !isFlipRight;
    }
    // Điều khiển trọng lực
    private void Gravity(){
        if(rb.linearVelocity.y<0){
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Math.Max(rb.linearVelocity.y,-maxFallSpedd));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    private void OnJump(InputValue value)
    {
        if(isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }
    }
    // Kiểm tra nếu nhân vật chạm đất
    private void CheckDown()
    {
        Collider2D checkGround = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.3f,0.02f),0f, groundLayer);
        if(checkGround != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Collider2D checkEnemy = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.3f,0.02f),0f, enemyLayer);
        if(checkEnemy != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce/2);
            checkEnemy.GetComponent<EnemyPatrol>().Die();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheck.position, new Vector3(.3f,0.02f,0));
    }
}