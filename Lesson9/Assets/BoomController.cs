using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    public float power;
    public float radius;
    SphereCollider coll;
    // Start is called before the first frame update
    void Boom()
    {
        Rigidbody[] r = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody item in r)
        {
            float dist = Vector3.Distance(item.transform.position, transform.position);
            if (dist < radius)
            {
                Vector3 dir = item.transform.position - transform.position;
                item.AddForce(dir.normalized * power * (radius - dist), ForceMode.Impulse);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Polygon")
        {
            Boom();
            Destroy(gameObject);
        }
    }
}
