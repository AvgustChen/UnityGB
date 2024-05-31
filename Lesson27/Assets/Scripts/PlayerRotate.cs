using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] GameObject sword;
    Vector3 mousePosition;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, 5 * Time.deltaTime);
        // Тот самый поворот
        // вычисляем разницу между текущим положением и положением мыши
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();
        // вычисляем необходимый угол поворота
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // применяем поворот вокруг оси Z
        transform.rotation = Quaternion.Euler(rotation_z, 0f, 0f);
    }
}
