using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CorutinesSample : MonoBehaviour
{
    int index;
    Coroutine coroutine;
    void Start()
    {
        coroutine = StartCoroutine(timer());

    }

    void Update()
    {
        if(index == 5)
        {
            StopCoroutine(coroutine);
        }
    }
    IEnumerator timer()
    {
        for (int i = 0; i <= 10; i++)
        {

            yield return new WaitForSeconds(1);
            if(i == 3)
                continue;
            if(i == 5)
                index = 5;
            Debug.Log("Ping " + i);
        }

    }
}
