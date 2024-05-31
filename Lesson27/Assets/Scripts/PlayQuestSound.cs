using UnityEngine;
using UnityEngine.Playables;

public class PlayQuestSound : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    [SerializeField] GameObject text;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sound.Play();
            text.SetActive(true);
            Destroy(text, 3);
            GetComponent<BoxCollider>().enabled = false;
            if(GetComponent<PlayableDirector>())
                GetComponent<PlayableDirector>().Play();
        }
    }
}
