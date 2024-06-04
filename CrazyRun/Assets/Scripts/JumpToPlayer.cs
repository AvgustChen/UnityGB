using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JumpToPlayer : MonoBehaviour
{
    [SerializeField] AudioSource stackAudio;
    GuiGame gui;
    bool coroutineStart;

    void Start()
    {
        gui = GameObject.Find("GuiGame").GetComponent<GuiGame>();
    }

    IEnumerator ProductsToPlayer(GameObject bag)
    {
        var playerBag = bag.GetComponent<PlayerBag>();
        coroutineStart = true;

        foreach (Transform item in transform)
        {
            if (playerBag.bagPlace.transform.childCount < gui.maxWeight)
            {
                Instantiate(stackAudio, transform.position, Quaternion.identity);
                item.transform.DOJump(playerBag.bagPlace.position, 5f, 1, 0.2f);
                item.transform.parent = playerBag.bagPlace;
            }
            else
            {
                coroutineStart = false;
                StopCoroutine(ProductsToPlayer(bag));
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        coroutineStart = false;
        StopCoroutine(ProductsToPlayer(bag));
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Helper"))
        {
            GameObject bag = other.gameObject;
            if (transform.childCount > 0)
            {
                if (!coroutineStart)
                {
                    StartCoroutine(ProductsToPlayer(bag));
                }
            }
        }
    }
}
