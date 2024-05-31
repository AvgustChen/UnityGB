
using Unity.VisualScripting;
using UnityEngine;

public class GetDamage : MonoBehaviour
{

    [SerializeField] GameObject orcHit;
    void OnTriggerEnter(Collider other)
    {
        GameObject player = GameData.player;
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemies>();
            if (!enemy.dead)
                Instantiate(orcHit, transform.position, Quaternion.identity);
            if (enemy.health - 25 >= 0)
                enemy.health -= 25;
            else
            {
                enemy.health -= enemy.health;
            }
        }
        if (other.gameObject.CompareTag("Troll"))
        {
            var enemy = other.gameObject.GetComponent<Troll>();
            if (!enemy.dead)
                Instantiate(orcHit, transform.position, Quaternion.identity);
            if (enemy.health - 25 >= 0)
                enemy.health -= 25;
            else
            {
                enemy.health -= enemy.health;
            }
        }
        if (other.gameObject.CompareTag("OrcHouse"))
        {
            var enemy = other.gameObject.GetComponent<OrcHouse>();
            if (enemy.health - 25 >= 0)
                enemy.health -= 25;
            else
            {
                enemy.health -= enemy.health;
                enemy.expl.SetActive(true);
                enemy.fire.SetActive(true);
                Destroy(other.gameObject, 4);
            }
        }
    }
}
