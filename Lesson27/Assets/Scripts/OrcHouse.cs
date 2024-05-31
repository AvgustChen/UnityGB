using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrcHouse : MonoBehaviour
{
    [SerializeField] Image lifeBar;
    [SerializeField] public float health;
    float maxHealth;
    [SerializeField] GameObject enemy;
    [SerializeField] int count;
    [SerializeField] Transform point;
    public GameObject expl;
    public GameObject fire;
    [SerializeField] AudioSource orcSound;

    void Start()
    {
        lifeBar = lifeBar.GetComponent<Image>();
        maxHealth = health;
    }

    void Update()
    {
        lifeBar.fillAmount = health / maxHealth;
        if(health <= 0)
        {
            StopAllCoroutines();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CreateEnemy());
            GetComponent<SphereCollider>().enabled = false;
        }
    }
    IEnumerator CreateEnemy()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(2);
            Instantiate(enemy, point.position, Quaternion.identity);
            Instantiate(orcSound, transform.position,Quaternion.identity);
        }
    }
}
