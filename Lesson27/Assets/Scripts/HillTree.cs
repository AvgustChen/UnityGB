using UnityEngine;

public class HillTree : MonoBehaviour
{
    [SerializeField] ParticleSystem hill;
    [SerializeField] ParticleSystem onOffParticl;
    float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            onOffParticl.Stop();
        }
        else if (timer < 0)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            if (!onOffParticl.isPlaying)
                onOffParticl.Play();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hill.Play();
            if (timer < 0)
            {
                if (GameData.player.GetComponent<Player>().health + 100 <= GameData.player.GetComponent<Player>().maxHealth)
                {
                    GameData.player.GetComponent<Player>().health += 100;
                }
                else
                {
                    GameData.player.GetComponent<Player>().health += GameData.player.GetComponent<Player>().maxHealth - GameData.player.GetComponent<Player>().health;
                }
                timer = 15;
            }

        }
    }
}
