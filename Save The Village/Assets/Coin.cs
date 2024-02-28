using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit != false)
                {
                    if (hit.transform.gameObject.layer == 5)
                    {
                        Money.money += 1;
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}
