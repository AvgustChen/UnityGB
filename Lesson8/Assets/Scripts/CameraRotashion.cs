using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRotashion : MonoBehaviour
{
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 10, 0), 0.1f * Time.deltaTime);
    }
}
