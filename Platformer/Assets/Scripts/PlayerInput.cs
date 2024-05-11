using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float horizontalDirection = Input.GetAxisRaw(GlobalStringVargs.HORIZONTAL_AXIS);
        //float verticalDirection = Input.GetAxisRaw(GlobalStringVargs.VERTICAL_AXIS);
        bool isJumpButtonPressed = Input.GetButtonDown(GlobalStringVargs.JUMP);
        playerMovement.Move(horizontalDirection, isJumpButtonPressed);
    }

}
