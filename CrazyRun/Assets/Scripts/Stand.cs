using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stand : MonoBehaviour
{
    [SerializeField] public GameObject objType;
    [SerializeField] public Transform[] places;
    [SerializeField] TMP_Text countText;
    [SerializeField] public GameObject products;
    [SerializeField] AudioSource stackAudio;
    public Image image;
    public int index;
    public int count;
    bool coroutineStart;
    public SaveGame saveGame;

    public void Start()
    {
        saveGame = saveGame.GetComponent<SaveGame>();
        // if(count > 0)
        // {
        //     for(int i = 0; i < count; i++)
        //     {
        //         GameObject c = Instantiate(objType, transform.position, Quaternion.identity);
        //         c.transform.DOJump(places[index].position, 5f, 1, 0.2f);
        //         //c.transform.rotation = Quaternion.Euler(0, 0, 0);
        //         c.transform.parent = products.transform;
        //         //c.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //         index++;
        //         if (index == 12) index = 0;
        //     }
        // }
    }

    void Update()
    {
        if (index == 12) index = 0;
        if (index < 0) index = 11;

        countText.text = count.ToString();
    }
    IEnumerator Products(GameObject bag)
    {

        coroutineStart = true;

        foreach (Transform item in bag.transform)
        {
            if (item.tag == objType.tag)
            {
                Instantiate(stackAudio, transform.position, Quaternion.identity);
                item.transform.DOJump(places[index].position, 5f, 1, 0.2f);
                item.transform.parent = products.transform;
                item.transform.rotation = Quaternion.Euler(0, 0, 0);
                index++;
                count++;
                if (index == 12)
                {
                    index = 0;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        coroutineStart = false;
        StopCoroutine(Products(bag));

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Helper"))
        {
            GameObject bag = other.gameObject.transform.Find("Bag").gameObject;

            if (bag.transform.childCount != 0)
            {
                if (!coroutineStart)
                    StartCoroutine(Products(bag));
                saveGame.Save();
            }
        }
        else if (other.CompareTag("Buyer"))
        {
            GameObject bag = other.gameObject.transform.Find("Bag").gameObject;

            if (bag.transform.childCount == 0)
            {
                if (other.GetComponent<Buyer>().wishProduct == objType)
                {
                    if (products.transform.childCount > 0)
                    {
                        Instantiate(stackAudio, transform.position, Quaternion.identity);
                        products.transform.GetChild(products.transform.childCount - 1).transform.DOJump(bag.transform.position, 5f, 1, 0.2f);
                        products.transform.GetChild(products.transform.childCount - 1).transform.parent = bag.transform;
                        index--;
                        count--;
                        saveGame.Save();
                    }
                }
            }
        }
    }
}
