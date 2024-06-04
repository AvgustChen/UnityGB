using UnityEngine;

public class AudioDestroyer : MonoBehaviour
{

    void Start()
    {
        Invoke("DestAudio", 1f);
    }

    void DestAudio()
    {
        Destroy(gameObject);
    }
}
