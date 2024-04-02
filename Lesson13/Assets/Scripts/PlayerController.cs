using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject propeller;
    public GameObject exploshion;
    public GameObject exploshionSound;
    public GameObject smoke1;
    public GameObject smoke2;
    public GameObject smoke3;
    public GameObject smoke4;
    public GameObject smokeFire;
    public Joystick joystick;
    public float flySpeed;
    private float horizontalMovement;
    private float amount = 30;
    public bool isPlayer;
    public int life = 1000;
    public AudioSource fallSound;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start(){
        Time.timeScale = 0;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * flySpeed * Time.deltaTime);
        if (isPlayer)
        {

            float horizontal = joystick.Horizontal;
            float vertical = -joystick.Vertical;

            horizontalMovement += horizontal * amount * Time.deltaTime;
            float verticalMovement = Mathf.Lerp(0, 20, Mathf.Abs(vertical)) * Mathf.Sign(vertical);
            float roll = Mathf.Lerp(0, 30, Mathf.Abs(horizontal)) * -Mathf.Sign(horizontal);

            transform.localRotation = Quaternion.Euler(Vector3.up * horizontalMovement + Vector3.right * verticalMovement + Vector3.forward * roll);
        }
        if (life < 450) smoke1.SetActive(true);
        if (life < 300) smoke2.SetActive(true);
        if (life < 200) smoke3.SetActive(true);
        if (life < 100) smokeFire.SetActive(true);
        if (life <= 0)
        {
            rb.AddForce(Vector3.forward, ForceMode.Impulse);
            transform.Rotate(0, 0, 5);
            if (!fallSound.GetComponent<AudioSource>().isPlaying)
            {
                fallSound.GetComponent<AudioSource>().Play();
            }
            smoke4.SetActive(true);

            if (propeller)
            {
                propeller.GetComponent<ConfigurableJoint>().connectedBody = null;
                Destroy(propeller);
            }
            rb.isKinematic = false;
            Invoke("Dead", 3.5f);
        }

    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Enemy")){
            SceneManager.LoadScene(0);
        }
    }

    void Dead()
    {
        Instantiate(exploshion, transform.position, Quaternion.identity);
        Instantiate(exploshionSound, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void StartGame(){
        Time.timeScale = 1;
    }
}
