using Unity.VisualScripting;
using UnityEngine;

public class TroolsFire : MonoBehaviour
{
    [SerializeField] float forceDamage;
    public Vector3 target;

    void Update()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, 7 * Time.deltaTime);
        if(transform.position == target)
        {
            Destroy(gameObject);
        }
    }
    public void Attack(Vector3 player)
    {
        target = player;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var healthPl = other.GetComponent<Player>();
            if (healthPl.health - forceDamage >= forceDamage)
            {
                healthPl.health -= forceDamage;
            }
            else
            {
                healthPl.health -= healthPl.health;
            }
            Destroy(gameObject);
        }

    }
}
