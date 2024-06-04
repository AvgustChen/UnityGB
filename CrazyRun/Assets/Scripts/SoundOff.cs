using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOff : MonoBehaviour
{
    public AudioSource music;
    public AudioSource[] sounds;
    public GameObject botton1;
    public GameObject botton2;

    public bool musicOff;
    public bool soundOff;

    public void MusicOff()
    {
        if (music.volume == 0)
        {
            music.volume = 0.5f;
            musicOff = false;
            botton1.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        else
        {
            music.volume = 0;
            musicOff = true;
            botton1.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
        }
    }
    public void SoundsOff()
    {
        foreach (AudioSource item in sounds)
        {
            if (item.volume == 0)
            {
                item.volume = 1;
                soundOff = false;
                botton2.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            else
            {
                item.volume = 0;
                soundOff = true;
                botton2.gameObject.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
            }
        }
    }

    public void MUsicRewars()
    {
        music.volume = 0;
        foreach (AudioSource item in sounds)
        {
            item.volume = 0;
        }
    }
    public void MUsicOnRewars()
    {
        if (!musicOff)
        {
            music.volume = 0.5f;
        }
        if (!soundOff)
        {
            foreach (AudioSource item in sounds)
            {
                item.volume = 1;
            }
        }
    }
}
