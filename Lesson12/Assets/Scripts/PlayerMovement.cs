using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float speed = 2;
    private Rigidbody rb;
    [SerializeField] ParticleSystem death;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveCharacter(Vector3 movement)
    {
        rb.AddForce(movement * speed);
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Instantiate(death, transform.position, Quaternion.identity);
            Invoke("Restart", 0.5f);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Reset values")]
    public void ResetValues()
    {
        speed = 2;
    }
#endif
}
