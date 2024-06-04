using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public Transform bagPlace;

    void Update()
    {
        if (bagPlace.transform.childCount > 0)
        {
            for (int i = 0; i < bagPlace.transform.childCount; i++)
            {
                if(bagPlace.transform.GetChild(i).transform.position != bagPlace.transform.position)
                    bagPlace.transform.GetChild(i).transform.position = bagPlace.transform.position;

            }
        }
    }

}
