using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [HideInInspector] public int maxTime;
    [SerializeField] Text timerDisplay;
    [SerializeField] Text EnemyCountDisplay;
    [SerializeField] Text WariorCountDisplay;
    [SerializeField] Text MoneyCountDisplay;
    [SerializeField] Text WheatCountDisplay;
    [SerializeField] Text WheatCountLevel;
    [SerializeField] Text UpgradeCost;
    [SerializeField] Image img;
    [SerializeField] GameObject warriors;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject LosePanel;
    [SerializeField] Transform pointEnemy;
    [SerializeField] Transform pointWarriors;
    [SerializeField] Button takeWheats;
    [SerializeField] Button WheatsUpgrade;
    [SerializeField] Button buyWarrior;
    private float currentTime;
    private int enemyCount;
    private int wheat;
    private int costWheat;
    private float currentTimeWheats;

    void Start()
    {
        costWheat = 1;
        maxTime = 10;
        currentTimeWheats = 10;
        enemyCount = 5;
        Money.wariorCount = 2;
        wheat = 0;
        img = img.GetComponent<Image>();
        currentTime = maxTime;
    }

    void Update()
    {
        UpgradeCost.text = "Улучшить за " + costWheat.ToString();
        currentTimeWheats -= Time.deltaTime;
        if (currentTimeWheats <= 0)
        {
            takeWheats.GetComponent<Button>().interactable = true;
        }
        else
        {
            takeWheats.GetComponent<Button>().interactable = false;
        }

        if (Money.money > 0 && wheat > 0)
        {
            buyWarrior.GetComponent<Button>().interactable = true;
        }
        else
        {
            buyWarrior.GetComponent<Button>().interactable = false;
        }

        if (Money.money > costWheat)
        {
            WheatsUpgrade.GetComponent<Button>().interactable = true;
        }
        else
        {
            WheatsUpgrade.GetComponent<Button>().interactable = false;
        }

        MoneyCountDisplay.text = Money.money.ToString();
        WheatCountDisplay.text = wheat.ToString();
        WariorCountDisplay.text = Money.wariorCount.ToString();
        EnemyCountDisplay.text = enemyCount.ToString();

        GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] w = GameObject.FindGameObjectsWithTag("Player");
        if (w.Length == 0 && e.Length > 0)
        {
            LosePanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (e.Length == 0)
        {
            foreach (var item in w)
            {
                Destroy(item);
            }
            currentTime -= Time.deltaTime;
            timerDisplay.text = "До нападения " + Math.Round(currentTime).ToString() + " сек";
        }
        else
        {
            timerDisplay.text = "Атака!";
        }

        if (currentTime <= 0)
        {
            currentTime = 60;
            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(enemy, pointEnemy.transform.position, Quaternion.identity);
            }
            for (int i = 0; i < Money.wariorCount; i++)
            {
                Instantiate(warriors, pointWarriors.transform.position, Quaternion.identity);
            }
            enemyCount += 4;

        }
        else
        {
            img.fillAmount = currentTime / maxTime;
        }
    }

    public void BuyWarriorButton()
    {
        Money.wariorCount += 1;
        Money.money -= 1;
        wheat -= 1;
    }

    public void TakeWheats()
    {
        wheat += Convert.ToInt32(WheatCountLevel.text);
        currentTimeWheats = 10;
    }

    public void UpgradeWheats()
    {
        WheatCountLevel.text = (Convert.ToInt32(WheatCountLevel.text) + 1).ToString();
        Money.money -= costWheat;
        costWheat += 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}