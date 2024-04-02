using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 2;
    
    public GameObject particle;

    void Awake()
    {
        Destroy(gameObject, bulletLife);
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Enemy")){
            GameObject v = Instantiate(particle, transform.position, Quaternion.identity);
            v.transform.parent = other.gameObject.transform;
            other.GetComponent<PlayerController>().life -= 1;
            Destroy(gameObject);
        }
    }
}
