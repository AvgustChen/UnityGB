using UnityEngine;
using UnityEngine.UI;

public class Troll : MonoBehaviour
{
    GameObject player;
    Animator anim;
    public float health;
    public Image barLife;
    float maxLife;
    [SerializeField] GameObject fire;
    [SerializeField] Transform pointFire;
    [SerializeField] GameObject DeadSound;
    public bool dead;

    void Start()
    {
        maxLife = health;
        anim = GetComponent<Animator>();
        barLife = barLife.GetComponent<Image>();

    }


    void Update()
    {
        player = GameData.player;
        barLife.fillAmount = health / maxLife;
        if (health <= 0 && !dead)
        {
            dead = true;
            GameData.count += 1;
            anim.SetTrigger("isDead");
            Instantiate(DeadSound, transform.position, Quaternion.identity);
            Destroy(gameObject, 3);
        }
        else
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < 35 && dist > 25 && health > 0)
            {
                anim.SetBool("isChasing", true);
                transform.LookAt(player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
            }
            else if (dist < 25 && dist > 0 && health > 0)
            {
                transform.LookAt(player.transform.position);
                anim.SetBool("isChasing", false);
                anim.SetBool("isAttack", true);

            }
            else
            {
                anim.SetBool("isAttack", false);
            }
        }
    }

    // void OnTriggerStay(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         anim.SetBool("isAttack", true);
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         anim.SetBool("isAttack", false);
    //     }
    // }

    void Attack()
    {
        GameObject bullet = Instantiate(fire, pointFire.position, Quaternion.identity);
        bullet.GetComponent<TroolsFire>().Attack(player.transform.position);
    }


}
