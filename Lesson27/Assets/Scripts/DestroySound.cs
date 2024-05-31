using UnityEngine;

public class DestroySound : MonoBehaviour
{
    float timer = 2;


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
