using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Fire : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool pressed;
    public GameObject shootSound;
    public GameObject bullet;
    public Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    public Transform[] spawnBullet;
    public GameObject particles;
    AudioSource v;

    public float shootForce;
    public float spread;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
    void Start()
    {
        cinemachineVirtualCamera = cinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>();
        v = shootSound.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 
                Mathf.Lerp(startingIntensity, 0f, 1-(shakeTimer / shakeTimerTotal));

        }

        if (pressed)
        {
            if (!v.isPlaying)
            {
                v.Play();
            }
            Shoot();
        }
        else
        {
            v.Stop();
        }
    }
    public void Shoot()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Enemy")
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(275);
            }
        }
        else
        {
            targetPoint = ray.GetPoint(275);
        }

        int rand = Random.Range(0, spawnBullet.Length);
        Vector3 dirWhithoutSpread = targetPoint - spawnBullet[rand].position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 dirWithSpread = dirWhithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, spawnBullet[rand].position, Quaternion.identity);
        currentBullet.transform.forward = dirWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(dirWithSpread * shootForce, ForceMode.Impulse);
        ShakeCamera(3f, .1f);

        GameObject firePartical = Instantiate(particles, spawnBullet[rand].position, Quaternion.identity);
        firePartical.transform.parent = spawnBullet[rand];
    }

    public void ShakeCamera(float intesity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intesity;
        startingIntensity = intesity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }



}
