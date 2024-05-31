
using Cinemachine;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float speed = 5f;
    Animator anim;
    [SerializeField] AudioSource stepSound;
    [SerializeField] CinemachineVirtualCamera cameraForvard;
    [SerializeField] CinemachineVirtualCamera cameraBack;
    //bool ground;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        var run = transform.position;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            cameraForvard.gameObject.SetActive(true);
            cameraBack.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
            cameraForvard.gameObject.SetActive(false);
            cameraBack.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.down * speed * 14 * Time.deltaTime);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up * speed * 14 * Time.deltaTime);


        if (run != transform.position)
        {
            anim.SetBool("isRun", true);
            if (!stepSound.isPlaying)
                stepSound.Play();
        }
        else
        {
            anim.SetBool("isRun", false);
            stepSound.Stop();
        }
    }

    // void OnCollisionExit(Collision collision)
    // {
    //     if (collision.collider.gameObject.layer == 3)
    //     {
    //         ground = false;
    //     }
    // }
    // void OnCollisionStay(Collision collision)
    // {
    //     if (collision.collider.gameObject.layer == 3)
    //     {
    //         ground = true;
    //     }
    // }

}
