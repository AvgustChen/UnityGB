using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float speed = 2;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveCharacter(Vector3 movement)
    {
        rb.AddForce(movement * speed);
    }

#if UNITY_EDITOR
    [ContextMenu("Reset values")]
    public void ResetValues()
    {
        speed = 2;
    }
#endif
}
