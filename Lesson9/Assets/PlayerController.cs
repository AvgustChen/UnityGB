using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float speed;
    [SerializeField] float moveHorizontal;
    [SerializeField] float moveVertical;
    [SerializeField] Transform pointFire;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Rotate(0, Input.GetAxis("Mouse X") * speed, 0);

        transform.Translate(moveHorizontal, 0, moveVertical);

        if (Input.GetMouseButtonDown(0)){

                    GameObject bul = Instantiate(bullet, pointFire.transform.position, Quaternion.identity);
                    bul.GetComponent<Rigidbody>().AddForce(pointFire.forward * 15, ForceMode.Impulse);
               
        }


    }
}
