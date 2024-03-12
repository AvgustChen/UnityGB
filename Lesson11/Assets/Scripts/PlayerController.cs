using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float yRot;
    public float yAngle;
    Quaternion rot;
    float horizontalInput;
    public float speed;
    public float speedRotation;

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis(InputsConst.horizontalInput);
        transform.Translate(horizontalInput * speed * Time.deltaTime, 0f, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speedRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            other.transform.parent.GetComponent<Animator>().SetTrigger("GetIt");
            Destroy(other.transform.parent.gameObject, 0.4f);
        }
        if (other.CompareTag("Right"))
        {
            yRot += yAngle;
        }
        else if (other.CompareTag("Left"))
        {
            yRot -= yAngle;
        }
        rot = Quaternion.Euler(0, yRot, 0);
    }
}
