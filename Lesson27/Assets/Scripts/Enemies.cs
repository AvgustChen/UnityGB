using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    GameObject player;
    Animator anim;
    public float health;
    public Image barLife;
    float maxLife;
    [SerializeField] float forceDamage;
    [SerializeField] GameObject DeadSound;
    public bool dead;


    void Start()
    {
        maxLife = health;
        anim = GetComponent<Animator>();
        barLife = barLife.GetComponent<Image>();
        player = GameData.player;
    }


    void Update()
    {
        barLife.fillAmount = health / maxLife;
        if (health <= 0 && !dead)
        {

            dead = true;
            GameData.count += 1;
            anim.SetTrigger("isDead");
            Instantiate(DeadSound, transform.position, Quaternion.identity);
            Destroy(gameObject, 4);
        }
        else
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < 10 && dist > 1 && health > 0)
            {
                anim.SetBool("isChasing", true);
                transform.LookAt(player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 3 * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isChasing", false);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isAttack", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isAttack", false);
        }
    }

    public void GetDamage()
    {
        var healthPl = player.GetComponent<Player>();
        if (healthPl.health - forceDamage >= forceDamage)
        {
            healthPl.health -= forceDamage;
        }
        else
        {
            healthPl.health -= healthPl.health;
        }
    }


}
