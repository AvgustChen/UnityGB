using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public float Speed = 10f;
    Rigidbody2D rb;
    public GameObject imgMoney;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(1f, 0.0f, 0f);

        rb.AddForce(movement * Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Instantiate(imgMoney, transform.position, Quaternion.identity);
            Money.wariorCount -= 1;
            Destroy(gameObject);
        }
    }
}
