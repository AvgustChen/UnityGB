using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using YG;

public class BuyTable : MonoBehaviour
{
    public Transform moneyPlace;
    public GameObject Money;
    public TMP_Text countMoneyText;
    public AudioSource MoneySound;
    public AudioSource MoneySound2;
    public GuiGame gui;
    bool takeMoney;
    void Start()
    {

        gui = gui.GetComponent<GuiGame>();

    }

    void Update()
    {
        countMoneyText.text = YandexGame.savesData.countTable.ToString();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Buyer"))
        {
            var buyer = other.GetComponent<Buyer>();
            if (!buyer.buy)
            {
                buyer.buy = true;
                Instantiate(MoneySound, transform.position, Quaternion.identity);
                for (int i = 0; i < other.GetComponent<Buyer>()._CurrentWaypointIndex + 1; i++)
                {
                    GameObject m = Instantiate(Money, other.transform.position, Quaternion.identity);
                    m.transform.DOJump(moneyPlace.position, 5f, 1, 0.2f);
                    m.transform.parent = moneyPlace.transform;
                    m.transform.rotation = Quaternion.Euler(90, 0, 0);
                    gui.countProgress += 10;
                    YandexGame.savesData.countTable += 5;
                    YandexGame.savesData.leaderBoard += 10;
                    YandexGame.NewLeaderboardScores("Leaders", YandexGame.savesData.leaderBoard);
                }
                YandexGame.SaveProgress();
                buyer._agent.SetDestination(buyer.GoAway.position);
            }
        }
        if (other.CompareTag("Player"))
        {
            if (moneyPlace.transform.childCount > 0 && !takeMoney)
            {
                takeMoney = true;
                foreach (Transform item in moneyPlace.transform)
                {
                    if (YandexGame.savesData.countTable - 5 >= 0)
                    {
                        item.DOJump(other.transform.Find("Bag").position, 5f, 1, 0.2f).OnComplete(() =>
                    {
                        Destroy(item.gameObject);
                    });
                        item.transform.parent = other.transform;
                        gui.money += 5;
                        YandexGame.savesData.countTable -= 5;
                    }
                }
                Instantiate(MoneySound2, transform.position, Quaternion.identity);
                takeMoney = false;

            }
        }
    }

}
