using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Text bestScore;
    public Sprite SoundOn;
    public Sprite SoundOff;
    public Sprite MusicOn;
    public Sprite MusicOff;

    AudioSource SoundOnClick;
    AudioSource MusicOnClick;

    public GameObject OptionsMenu;
    public GameObject ShopMenu;
    bool musicPlayCheck;
    void Start()
    {
        SoundOnClick = GameObject.Find("Audio Soudn On Click").GetComponent<AudioSource>();
        MusicOnClick = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        LoadAudio();
        bestScore.text = PlayerPrefs.GetInt("bestscore").ToString();
    }

    public void ClickStart() 
    {
        SceneManager.LoadScene("SampleScene");
        SoundOnClick.Play();
    }

    public void ClickOption()
    {
        OptionsMenu.SetActive(true);
        SoundOnClick.Play();
    }

    public void CLickShop() 
    {
        ShopMenu.SetActive(true);
        SoundOnClick.Play();
    }

    public void SoundClick()
    {
        if (SoundOnClick.volume == 0f)
        {
            GameObject.Find("Sound Btn").GetComponent<Image>().sprite = SoundOn;
            SoundOnClick.volume = 0.301f;
        }
        else
        {
            GameObject.Find("Sound Btn").GetComponent<Image>().sprite = SoundOff;
            SoundOnClick.volume = 0f;
        }
        SoundOnClick.Play();
    }

    public void MusicClick()
    {
        if (MusicOnClick.volume==0f)
        {
            GameObject.Find("Music Btn").GetComponent<Image>().sprite = MusicOn;
            //MusicOnClick.Play();
            MusicOnClick.volume = 0.502f;
        }
        else
        {
            GameObject.Find("Music Btn").GetComponent<Image>().sprite = MusicOff;
            // MusicOnClick.Pause();
            MusicOnClick.volume = 0f;
        }
        SoundOnClick.Play();
    }

    public void BackClickOption()
    {
        OptionsMenu.SetActive(false);
        SaveSoundData();
        SaveMusicData();
        SoundOnClick.Play();
    }
    
    public void BackClickShop()
    {
        ShopMenu.SetActive(false);
        SoundOnClick.Play();
    }

    void SaveSoundData() {
        PlayerPrefs.SetString("sound",Convert.ToString(SoundOnClick.volume));
    }

    void SaveMusicData() {
        PlayerPrefs.SetString("music", Convert.ToString(MusicOnClick.volume));
    }

    void LoadAudio() {
        SoundOnClick.volume = float.Parse(PlayerPrefs.GetString("sound"));
        MusicOnClick.volume = float.Parse(PlayerPrefs.GetString("music"));
    }

    void Update()
    {
        if (MusicOnClick.volume == 0f)
        {
            GameObject.Find("Music Btn").GetComponent<Image>().sprite = MusicOff;
        }
        else
        {
            GameObject.Find("Music Btn").GetComponent<Image>().sprite = MusicOn;
        }

        if (SoundOnClick.volume == 0f)
        {
            GameObject.Find("Sound Btn").GetComponent<Image>().sprite = SoundOff;
        }
        else
        {
            GameObject.Find("Sound Btn").GetComponent<Image>().sprite = SoundOn;
        }
    }


}
