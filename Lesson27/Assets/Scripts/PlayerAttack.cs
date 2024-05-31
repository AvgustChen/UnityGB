using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject sword;
    [SerializeField] ParticleSystem effectAttack;
    [SerializeField] AudioSource hitSound;
    [SerializeField] GameObject swordLight;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.E))
        {
            if (sword.activeInHierarchy)
            {
                anim.SetTrigger("Attack");

            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash"))
        {
            swordLight.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            swordLight.GetComponent<BoxCollider>().enabled = false;
        }

    }

    public void AttackEffect()
    {
        effectAttack.Play();
        hitSound.Play();
    }
}
