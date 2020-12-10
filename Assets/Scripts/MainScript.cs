using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq.Expressions;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public Text scoreText;
    Image spriteR;
    Sprite lastSprite;

    public GameObject LoseMenu;

    public AudioSource Music;

    public Sprite GreenButton;
    public Sprite RedButton;
    public Sprite BlueButton;
    public Sprite GreenButtonPR;
    public Sprite RedButtonPR;
    public Sprite BlueButtonPR;
    public Button button;
    SpriteState ButtonSpriteState;
    float timer;
    int oldscore = 0;
    System.Random rnd = new System.Random();

    bool ButtonAlreadyClicked = false;

    double waitTime;
    int score = 0;
    bool lose = false;
    int bestScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestscore");
        Time.timeScale = 1f;
        spriteR = button.GetComponent<Image>();
        ButtonSpriteState = button.spriteState;
        waitTime = 1;
        lastSprite = RedButton;
        Debug.Log(waitTime + "  time = "+ timer);
        LoadSound();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

        timer += Time.deltaTime;
        Debug.Log(timer);

        if (waitTime <= Math.Round(timer, 1))
        {
            if (oldscore > score && lastSprite == GreenButton)
            {
                lose = true;
            }

            Respawn();
            double fTime = -0.51f*Math.Log(score)+3;
            waitTime = Math.Round(timer + fTime, 1);
            Debug.Log(fTime);
            lastSprite = spriteR.sprite;
            if (lastSprite == GreenButton) {
                oldscore++;
            }
        }

        if (ButtonAlreadyClicked) 
        {
            waitTime = Math.Round(timer,1);
            ButtonAlreadyClicked = false; 
        }

        if (lose == true)
        {
            Pause();
            Debug.Log(score);
            if (bestScore < score)
                PlayerPrefs.SetInt("bestscore", score);
        }

        button.spriteState = ButtonSpriteState;
    }

    void Respawn()
    {
        switch (rnd.Next(1, 6))
        {
            default:
                spriteR.sprite = GreenButton;
                ButtonSpriteState.pressedSprite = GreenButtonPR;
                break;
            case 4:
                spriteR.sprite = RedButton;
                ButtonSpriteState.pressedSprite = RedButtonPR;
                break;
        }

        if (score >= 5)
        {
            Teleport();
            if (score >= 10)
            {
                Scale();
                if (score > 15)
                {
                    switch (rnd.Next(1, 5))
                    {
                        default:
                            spriteR.sprite = GreenButton;
                            ButtonSpriteState.pressedSprite = GreenButtonPR;
                            break;
                        case 2:
                            spriteR.sprite = RedButton;
                            ButtonSpriteState.pressedSprite = RedButtonPR;
                            break;
                        case 3:
                            spriteR.sprite = BlueButton;
                            ButtonSpriteState.pressedSprite = BlueButtonPR;
                            break;
                    }
                }
            }
        }


    }


    public void Teleport()
    {
        float x = rnd.Next(-1, 1);
        float y = rnd.Next(-4, 4);
        button.transform.position = new Vector3(x, y, 0);
    }


    public void Scale()
    {
        float scale = rnd.Next(1, 4);
        button.transform.localScale = new Vector3((1 / scale), (1 / scale), 1);
    }


    public void Click()
    {
        if (spriteR.sprite == RedButton || spriteR.sprite == BlueButton)
        {
            lose = true;
        }

        if (spriteR.sprite == GreenButton)
        {
            score += 1;
            spriteR.sprite = RedButton;
            ButtonSpriteState.pressedSprite = RedButtonPR;
            ButtonAlreadyClicked = true;
        }
    }

    public void backClick()
    {
        
        SceneManager.LoadScene("Menu");
    }



    void Pause() 
    {
        LoseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueGame() 
    {
        lose = false;
        oldscore = score;
        Resume();
    }

    void Resume() 
    {
        LoseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void LoadSound() {
        Music.volume=float.Parse(PlayerPrefs.GetString("music"));
    }

}
