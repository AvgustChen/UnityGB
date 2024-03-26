using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class PlayerController : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioSource GetTime;
    public AudioSource fonMusic;
    public Joystick joystick;
    float horizontalInput;
    public float speed;
    Vector3 oldPosition;
    float totalDistance;
    public TMP_Text dist;
    public TMP_Text life;
    public TMP_Text coinsText;
    public TMP_Text finishText;
    public TMP_Text continueText;
    int costContinue;
    int coins;
    public Image timeFill;
    int maxTime;
    float currentTime;
    int health;
    float timeForRespawn;
    float startSpeed;
    public float currentSpeed;
    public GameObject FinishPanel;
    public GameObject StartPanel;
    public ParticleSystem Crash;
    bool game;


    void Awake()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        timeFill = timeFill.GetComponent<Image>();
        maxTime = 60;
        coins = YandexGame.savesData.coins;
        currentTime = maxTime;
        startSpeed = speed;
        currentSpeed = speed;
        health = 3;
        oldPosition = transform.position;
        StartPanel.SetActive(true);
        game = true;
        costContinue = 500;
        fonMusic.Pause();
        if (Application.isMobilePlatform)
        {
            joystick.gameObject.SetActive(false);
        }


    }

    void Update()
    {
        life.text = health.ToString();
        coinsText.text = coins.ToString();
        currentTime -= Time.deltaTime;
        timeFill.fillAmount = currentTime / maxTime;
        dist.text = Mathf.Round(totalDistance / 1000).ToString();
        if (game)
            totalDistance += Vector3.Distance(transform.position, oldPosition);
        if (timeForRespawn > 0)
        {
            timeForRespawn -= Time.deltaTime;
            speed = startSpeed - 2;
        }
        else
        {
            speed = currentSpeed;
        }
        if ((health <= 0 && game) || (currentTime <= 0 && game))
        {
            FinishPanel.SetActive(true);
            fonMusic.Pause();
            if (YandexGame.lang == "en")
                continueText.text = "Continue " + costContinue;
            else if (YandexGame.lang == "ru")
                continueText.text = "Продолжить " + costContinue;
            game = false;
            Time.timeScale = 0;
            YandexGame.savesData.coins = coins;
            if (Convert.ToInt32(dist.text) > YandexGame.savesData.distance)
            {
                YandexGame.savesData.distance = Convert.ToInt32(dist.text);
                YandexGame.NewLeaderboardScores("Leaders", Convert.ToInt32(dist.text));
                if (YandexGame.lang == "en")
                    finishText.text = "You have a new record, you have run - " + Convert.ToInt32(dist.text) + " meters";
                else if (YandexGame.lang == "ru")
                    finishText.text = "У вас новый рекорд, вы пробежали - " + Convert.ToInt32(dist.text) + " метров";

            }
            else
            {
                if (YandexGame.lang == "en")
                    finishText.text = "You ran through - " + Convert.ToInt32(dist.text) + " meters\n" +
                                          "Your record - " + YandexGame.savesData.distance + " meters";
                else if (YandexGame.lang == "ru")
                    finishText.text = "Вы пробежали - " + Convert.ToInt32(dist.text) + " метров\n" +
                                      "Ваш рекорд - " + YandexGame.savesData.distance + " метров";
            }
            YandexGame.SaveProgress();
        }


    }

    void FixedUpdate()
    {
        if (!Application.isMobilePlatform)
        {
            horizontalInput = Input.GetAxis(InputsConst.horizontalInput);
        }
        else
        {
            horizontalInput = joystick.Horizontal;
        }
        transform.Translate(horizontalInput * (speed / 2) * Time.deltaTime, 0f, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.transform.parent.GetComponent<Animator>().SetTrigger("GetIt");
            coins += 1;
            int rand = UnityEngine.Random.Range(0, sounds.Length);
            if (!sounds[rand].isPlaying)
                sounds[rand].Play();
            Destroy(other.transform.parent.gameObject, 0.4f);
        }
        if (other.CompareTag("Time"))
        {
            GetTime.Play();
            if (currentTime + 15 <= maxTime)
            {
                currentTime += 15;
            }
            else
            {
                currentTime = maxTime;
            }
            other.GetComponent<Animator>().SetTrigger("GetIt");
            Instantiate(Crash, transform.position, Quaternion.identity);
            Destroy(other.transform.parent.gameObject, 0.4f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (timeForRespawn <= 0)
            {
                Instantiate(Crash, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                health -= 1;
                timeForRespawn = 3;
                currentSpeed = speed;
            }
        }
        if (other.gameObject.tag == "Respawn")
        {
                        FinishPanel.SetActive(true);
            fonMusic.Pause();
            if (YandexGame.lang == "en")
                continueText.text = "Continue " + costContinue;
            else if (YandexGame.lang == "ru")
                continueText.text = "Продолжить " + costContinue;
            game = false;
            Time.timeScale = 0;
            YandexGame.savesData.coins = coins;
            if (Convert.ToInt32(dist.text) > YandexGame.savesData.distance)
            {
                YandexGame.savesData.distance = Convert.ToInt32(dist.text);
                YandexGame.NewLeaderboardScores("Leaders", Convert.ToInt32(dist.text));
                if (YandexGame.lang == "en")
                    finishText.text = "You have a new record, you have run - " + Convert.ToInt32(dist.text) + " meters";
                else if (YandexGame.lang == "ru")
                    finishText.text = "У вас новый рекорд, вы пробежали - " + Convert.ToInt32(dist.text) + " метров";

            }
            else
            {
                if (YandexGame.lang == "en")
                    finishText.text = "You ran through - " + Convert.ToInt32(dist.text) + " meters\n" +
                                          "Your record - " + YandexGame.savesData.distance + " meters";
                else if (YandexGame.lang == "ru")
                    finishText.text = "Вы пробежали - " + Convert.ToInt32(dist.text) + " метров\n" +
                                      "Ваш рекорд - " + YandexGame.savesData.distance + " метров";
            }
            YandexGame.SaveProgress();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        fonMusic.Play();
        if (Application.isMobilePlatform)
        {
            joystick.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        if (coins > costContinue)
        {
            coins -= costContinue;
            fonMusic.Play();
            game = true;
            costContinue *= 2;
            currentTime = 60;
            if (currentSpeed > 10)
                currentSpeed = 10;
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            health = 1;
            FinishPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ContinueADV()
    {
        currentTime = 60;
        if (currentSpeed > 10)
            currentSpeed = 10;
        health = 1;
        game = true;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        FinishPanel.SetActive(false);
        Time.timeScale = 1;
        fonMusic.Play();
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            game = true;
            fonMusic.Play();
        }
        else
        {
            Time.timeScale = 0;
            game = false;
            fonMusic.Pause();
        }
    }
}


