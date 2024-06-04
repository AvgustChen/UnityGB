using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WayPointsBuyer : MonoBehaviour
{
    public GameObject buyer;
    public Transform startPoint;
    public Transform[] waypoints;
    public Transform[] walkPoints;
    public Transform buyPoint;
    public Transform GoAway;
    public int maxBuyer;
    float timer;
    int count;

    void Start()
    {
        maxBuyer = 10;
        timer = 20;
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(0, 11);
            {
                if (count < maxBuyer)
                {
                    count++;
                    GameObject b = Instantiate(buyer, startPoint.position, Quaternion.identity);
                    b.transform.parent = gameObject.transform;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Buyer>().changeProduct == true)
        {
            count--;
            Destroy(other.gameObject);
        }
    }

}
