using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputs : MonoBehaviour
{
    private Vector3 movement;
    PlayerMovement plMove;

    void Awake()
    {
        plMove = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical).normalized;
    }

    void FixedUpdate()
    {
        plMove.MoveCharacter(movement);
    }
}
