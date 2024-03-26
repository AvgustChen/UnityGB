using UnityEngine;

public class LocGenerate : MonoBehaviour
{
    public GameObject[] location;
    public Transform locPos;
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            int rand = Random.Range(0, location.Length - 1);
            Instantiate(location[rand], locPos.transform.position, Quaternion.identity);
            other.GetComponent<PlayerController>().currentSpeed += 1;
        }
    }
}
