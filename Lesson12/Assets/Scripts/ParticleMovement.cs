using UnityEngine;
public class ParticleMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    void FixedUpdate()
    {
        transform.position = playerTransform.position;
    }
}
