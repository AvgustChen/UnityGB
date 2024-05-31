using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField] Image lifeBar; 
    [SerializeField] GameObject losePanel;
    [SerializeField] TMP_Text count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameData.player = gameObject;
        health = 500;
        maxHealth = health;
        lifeBar = lifeBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.fillAmount = health / maxHealth;
        if(health<=0)
            {
                Time.timeScale = 0;
                losePanel.SetActive(true);
            }
        count.text = GameData.count.ToString();
    }
}
