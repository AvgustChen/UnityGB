using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSheep : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        target = point1.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if (transform.position.z == point2.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);
        if (transform.position.z == point2.position.z)
        {
            target = point1.position;
        }
        else if (transform.position.z == point1.position.z)
        {
            target = point2.position;
        }
    }
}
