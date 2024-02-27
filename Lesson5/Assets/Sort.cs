using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sort : MonoBehaviour
{
    [SerializeField] InputField length;
    [SerializeField] Text arrayText;
    [SerializeField] Text resultText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    int[] SetArray(int len)
    {
        System.Random rand = new System.Random();
        int[] array = new int[len];
        for (int i = 0; i < len; i++)
        {
            array[i] = rand.Next(-100, 101);
        }
        return array;
    }
    void WriteArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (i == 0) { arrayText.text = array[i].ToString() + ", "; }
            else if (i != array.Length - 1) { arrayText.text += array[i].ToString() + ", "; }
            else arrayText.text += array[i].ToString();
        }
    }
    void WriteResultArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (i == 0) { resultText.text = array[i].ToString() + ", "; }
            else if (i != array.Length - 1) { resultText.text += array[i].ToString() + ", "; }
            else resultText.text += array[i].ToString();
        }
    }

    public void BubbleSort()
    {
        int len;
        if (int.TryParse(length.text, out len))
        {
            if (len > 0)
            {
                int[] array = SetArray(len);
                WriteArray(array);
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j] > array[j + 1])
                        {
                            (array[j + 1], array[j]) = (array[j], array[j + 1]);
                        }
                    }
                }
                WriteResultArray(array);
            }
            else
            {
                arrayText.text = "Вы ввели неправильное число!";
            }
        }
        else
        {
            arrayText.text = "Вы ввели неправильное число!";
        }

    }

    public void ChangeSort()
    {
        int len;
        if (int.TryParse(length.text, out len))
        {
            if (len > 0)
            {
                int[] array = SetArray(len);
                WriteArray(array);
                int min = 0;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    min = i;
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[j] < array[min])
                        {
                            min = j;
                        }
                    }
                    (array[i], array[min]) = (array[min], array[i]);

                }
                WriteResultArray(array);
            }
            else
            {
                arrayText.text = "Вы ввели неправильное число!";
            }
        }
        else
        {
            arrayText.text = "Вы ввели неправильное число!";
        }

    }
}
