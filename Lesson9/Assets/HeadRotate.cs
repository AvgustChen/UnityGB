using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    float speedRotation = 7f;
    float y = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var RotY = Input.GetAxis("Mouse Y");
        y -= RotY * speedRotation;
        y = Mathf.Clamp(y, -50, 50);
        transform.localRotation = Quaternion.Euler(y, 0, 0);


    }
}
