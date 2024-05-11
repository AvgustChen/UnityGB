using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundColliderTransform;
    [SerializeField] float speed;
    [SerializeField] LayerMask groundMask;
    private Rigidbody2D rb;
    float radius;
    bool isGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        radius = groundColliderTransform.gameObject.GetComponent<CircleCollider2D>().radius;
    }

    void FixedUpdate()
    {
        Vector3 overLapCirclePosition = groundColliderTransform.position;
        isGround = Physics2D.OverlapCircle(overLapCirclePosition, radius + 0.01f, groundMask);

    }
    public void Move(float direction, bool isJumpButtonPressed)
    {
        if (isJumpButtonPressed && isGround)
        {
            Jump();
        }
        if (direction != 0)
        {
            HorizontalMovement(direction);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void HorizontalMovement(float direction)
    {
        Debug.Log(direction);
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
