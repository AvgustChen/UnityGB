using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GuiGame : MonoBehaviour
{
    public int money;
    public int maxWeight;
    public int level;
    public int levelUpMoney;
    public Image progress;
    public TMP_Text progressText;
    public TMP_Text moneyText;
    public TMP_Text maxWeightText;
    public TMP_Text levelText;
    public float countProgress;
    public float maxProgress;
    public GameObject playerBag;
    public GameObject confety;
    public GameObject levelUpPanel;

    public TMP_Text moneyLevelUpText;
    public TMP_Text weightLevelUpText;
    public TMP_Text moneyRewardText;
    public SaveGame saveGame;
    public GameObject level6;
    public GameObject buyRoad;
    public GameObject level12;
    public GameObject buyRoad12;
    public GameObject resetButton;
    public GameObject player;
    public GameObject[] rewardsButtons;

    void Start()
    {
        saveGame = saveGame.GetComponent<SaveGame>();
        progress = progress.GetComponent<Image>();
        if (YandexGame.savesData.level == 0)
        {
            level = 1;
            money = 150;
            maxProgress = 100;
            maxWeight = 10;
            levelUpMoney = 60;

        }
        if (YandexGame.playerName == "Август")
        {
            resetButton.SetActive(true);
        }

    }


    void Update()
    {
        if (level < 2)
        {
            rewardsButtons[0].SetActive(false);
        }
        else
        {
            rewardsButtons[0].SetActive(true);
        }

        if (level >= 8 && !buyRoad.GetComponent<BuyPlace>().buyObj.activeInHierarchy)
        {
            level6.SetActive(false);
            buyRoad.SetActive(true);
        }

        if (level >= 15 && !buyRoad12.GetComponent<BuyPlace>().buyObj.activeInHierarchy)
        {
            level12.SetActive(false);
            buyRoad12.SetActive(true);
        }
        
        progressText.text = countProgress + " / " + maxProgress;
        levelText.text = level.ToString();
        moneyText.text = money.ToString();
        maxWeightText.text = playerBag.transform.childCount.ToString() + " / " + maxWeight.ToString();
        progress.fillAmount = countProgress / maxProgress;

        if (countProgress >= maxProgress)
        {
            if (GetComponent<Reklama>().timer < 10)
            {
                GetComponent<Reklama>().timer = 30;
            }
            levelUpPanel.SetActive(true);
            confety.SetActive(true);
            maxWeight += 1;
            moneyLevelUpText.text = levelUpMoney.ToString();
            weightLevelUpText.text = maxWeight.ToString();
            moneyRewardText.text = (levelUpMoney * 2).ToString();
            maxProgress += 200;
            countProgress = 0;
            level += 1;
            saveGame.Save();
        }

    }

    public void ClosePanel()
    {
        money += levelUpMoney;
        levelUpMoney += 30;
        player.GetComponent<PlayerMove>().canMove = true;
        saveGame.Save();
    }

    public void Reward()
    {
        player.GetComponent<PlayerMove>().canMove = true;
        money += (levelUpMoney * 2);
        levelUpMoney += 30;
        saveGame.Save();
    }




}
