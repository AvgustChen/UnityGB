using UnityEngine;
using UnityEngine.Playables;

public class TakeSword : MonoBehaviour
{
    public GameObject takeSwordText;
    public AudioSource takeSwordSound;
    public GameObject sword;
    float timerForDestroy;
    public GameObject gate;

    void Update()
    {
        if (timerForDestroy > 0)
            timerForDestroy -= Time.deltaTime;
        if (timerForDestroy < 0)
        {
            takeSwordText.SetActive(false);
            takeSwordSound.gameObject.SetActive(false);
        }
        //transform.Rotate(0, 0.5f, 0);
    }

    void OnCollisionEnter()
    {
        
        sword.SetActive(true);
        Destroy(gameObject);
        gate.transform.parent.GetComponent<BoxCollider>().enabled = false;
        Destroy(gate);
    }

    void OnTriggerEnter(Collider other)
    {
        timerForDestroy = 2;
        if (other.CompareTag("Player"))
        {
            takeSwordText.SetActive(true);
            if (!takeSwordSound.isPlaying)
            {
                takeSwordSound.Play();
            }
            GetComponent<PlayableDirector>().Play();
        }
        GetComponent<BoxCollider>().enabled = false;
    }
}
