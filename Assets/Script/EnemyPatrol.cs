using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed;
    [SerializeField] private Transform detecPos;
    [SerializeField] private float rayLength;
    [SerializeField] private float rayDistance;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask player;
    [SerializeField] private bool facingLeft = true;
    private bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(detecPos.position, Vector2.down, rayLength, ground);
        RaycastHit2D checkfontground = Physics2D.Raycast(detecPos.position, facingLeft ? Vector2.left : Vector2.right, rayDistance, ground);
        RaycastHit2D checkPlayer = Physics2D.Raycast(detecPos.position, facingLeft ? Vector2.left : Vector2.right, rayDistance, player);
        if(hit.collider==null||checkfontground.collider!=null)
        {
            anim.SetBool("isIdle", true);
            transform.Translate(Vector2.left * 0);
        }
        else
        {
            anim.SetBool("isIdle", false);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if(checkPlayer.collider!=null)
        {
            checkPlayer.collider.GetComponent<PlayerMovement>().Die();
        }
        
    }
    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        anim.SetTrigger("isDead");
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
    public void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawLine(detecPos.position, detecPos.position + Vector3.down * rayLength);
    Gizmos.DrawLine(detecPos.position, detecPos.position + (facingLeft ? Vector3.left : Vector3.right) * rayDistance);
}
}
