using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    [SerializeField] List<GameObject> crops;
    public float grow;
    public float time;
    public Image img;

    [SerializeField] GameObject crop;
    [SerializeField] Transform[] place;
    [SerializeField] GameObject stack;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform secondPosition;
    [SerializeField] AudioSource stackAudio;
    public bool startGrow;
    int index;


    void OnEnable()
    {
        grow = 0;
        foreach (var item in crops)
        {
            item.transform.localScale = new Vector3(0, 0, 0);
        }

    }


    void FixedUpdate()
    {
        if (startGrow)
        {
            img.gameObject.SetActive(true);
            if (grow <= 1)
            {
                grow = grow + Time.deltaTime / time;
                img.fillAmount = grow;
                foreach (var item in crops)
                {
                    item.transform.localScale = new Vector3(grow, grow, grow);
                }

            }
            else
            {
                startGrow = false;
            }
        }
        else
        {
            img.gameObject.SetActive(false);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Helper"))
        {
            if (grow >= 1)
            {
                grow = 0;
                StartCoroutine(Harvest());

            }
            else if (!startGrow)
            {
                other.GetComponent<Animator>().SetTrigger("Watering");
                startGrow = true;
            }
        }
    }

    public IEnumerator Harvest()
    {

        foreach (var item in crops)
        {
            item.transform.localScale = new Vector3(grow, grow, grow);
            Instantiate(stackAudio, startPosition.position, Quaternion.identity);
            GameObject cr = Instantiate(crop, startPosition.position, Quaternion.identity);
            cr.transform.rotation = Quaternion.Euler(0, 0, 90);
            cr.transform.DOJump(place[index].position, 5f, 1, 0.2f);
            cr.transform.parent = stack.transform;

            index++;
            if (index == 12)
            {
                index = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(Harvest());

    }
}
