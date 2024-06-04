using UnityEngine;
using YG;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    Rigidbody rb;
    public float speed;
    private Animator anim;
    public GameObject levelUpPanel;
    public GameObject rewardPanel;
    public bool canMove;
    Vector3 touch;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        canMove = true;
    }


    void FixedUpdate()
    {
        if (levelUpPanel.activeInHierarchy || rewardPanel.activeInHierarchy)
        {
            canMove = false;
        }

        if (canMove)
        {
            // if (Application.isMobilePlatform)
            // {
            if (Input.touchCount == 1)
            {
                // float horizontal = joystick.Horizontal;
                // float vertical = joystick.Vertical;
                Vector3 touch = Input.GetTouch(0).position;
                float horizontal = touch.x * Input.GetTouch(0).deltaPosition.x;
                float vertical = touch.y * Input.GetTouch(0).deltaPosition.y;
                Vector3 dir = new Vector3(horizontal, 0f, vertical);

                dir = Vector3.ClampMagnitude(dir, speed);
                if (dir != Vector3.zero)
                {
                    rb.AddForce(dir);
                    rb.MoveRotation(Quaternion.LookRotation(dir));
                    anim.SetBool("Run", true);
                }
            }
            else
            {
                anim.SetBool("Run", false);
            }
            //}
        }
    }


    void CanMove()
    {
        canMove = false;
    }

    void CanMoveTrue()
    {
        canMove = true;
    }
}
