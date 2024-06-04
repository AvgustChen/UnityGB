using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyPlace : MonoBehaviour
{
    public GameObject buyObj;
    public GameObject nextPlace;
    public GameObject HelperPlace;
    public GameObject money;
    public AudioSource moneySound;
    public int price;
    public int count;
    public TMP_Text PriceText;
    public GuiGame gui;
    bool coroutine;
    public SaveGame saveGame;
    bool helper;
    void Start()
    {
        saveGame = saveGame.GetComponent<SaveGame>();
        gui = gui.GetComponent<GuiGame>();

    }

    void Update()
    {
        if (buyObj.activeInHierarchy) gameObject.SetActive(false);
        PriceText.text = (price - count).ToString();
        if (count >= price)
        {
            if (buyObj.name == "Helper")
            {

                if (!helper)
                {
                    helper = true;
                    GameObject h = Instantiate(buyObj, transform.position, Quaternion.identity);
                    h.SetActive(true);
                    Invoke("ResetCount", 3);
                }
            }
            else
            {
                buyObj.SetActive(true);
                if (nextPlace != null)
                {
                    nextPlace.SetActive(true);
                }
                if (HelperPlace != null)
                {
                    HelperPlace.SetActive(true);
                }
                gameObject.SetActive(false);
            }
            saveGame.Save();
        }
    }

    IEnumerator TakeMoney(GameObject player)
    {
        coroutine = true;
        for (int i = 0; count < price; i++)
        {
            if (count + 10 <= price && gui.money - 10 >= 0)
            {
                Instantiate(moneySound, player.transform.position, Quaternion.identity);
                count += 10;
                gui.money -= 10;
                GameObject m = Instantiate(money, player.transform.position, Quaternion.identity);
                m.transform.DOJump(transform.position, 5f, 1, 0.2f)
                    .OnComplete(() =>
                    {
                        m.transform.parent = transform;
                        Destroy(m, 0.1f);
                    });

            }
            else if (count < price && gui.money > 0)
            {
                Instantiate(moneySound, player.transform.position, Quaternion.identity);
                count += 1;
                gui.money -= 1;
                GameObject m = Instantiate(money, player.transform.position, Quaternion.identity);
                m.transform.DOJump(transform.position, 5f, 1, 0.2f)
                    .OnComplete(() =>
                    {
                        m.transform.parent = transform;
                        Destroy(m, 0.1f);
                    });
            }
            else
            {
                coroutine = false;
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        coroutine = false;
        saveGame.Save();
        StopCoroutine(TakeMoney(player));

    }

    void ResetCount()
    {
        helper = false;
        count = 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gui.money > 0)
            {
                if (!coroutine) StartCoroutine(TakeMoney(other.gameObject));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coroutine = false;
        }
    }
}
