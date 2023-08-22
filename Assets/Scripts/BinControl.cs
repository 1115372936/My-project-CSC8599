using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class BinControl : MonoBehaviour
{
    public enum BinColour
    {
        Red = 0,
        Yellow = 1,
        Green = 2,
        Blue = 3,
    }

    public GameObject addScore;
    public GameObject minusScore;

    public BinColour binColour;

    Rigidbody2D rbody;

    private int blueScore = 0;
    private int redScore = 0;
    private int yellowScore = 0;
    private int greenScore = 0;

    private static int red = 0;
    private static int blue = 0;
    private static int green = 0;
    private static int yellow = 0;

    private float t = 0.3f;

    void Hidden()
    {
        addScore.SetActive(false);
        minusScore.SetActive(false);
    }

    public void SetRedScore(int s)
    {
        red = s;
    }

    public static int GetRedScore()
    {
        return red;
    }

    public void SetBlueScore(int s)
    {
        blue = s;
    }

    public static int GetBlueScore()
    {
        return blue;
    }

    public void SetGreenScore(int s)
    {
        green = s;
    }

    public static int GetGreenScore()
    {
        return green;
    }
    public void SetYellowScore(int s)
    {
        yellow = s;
    }

    public static int GetYellowScore()
    {
        return yellow;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubbishCollectable rubbish = other.gameObject.GetComponent<RubbishCollectable>();

        if (rubbish != null)
        {
            rubbish.Thrown();
            if(rubbish.belonging == binColour)
            {
                AudioManager.Instance.PlaySFX("Correct", false);

                addScore.SetActive(true);

                if(binColour == BinColour.Red)
                {
                    redScore += 50;
                    SetRedScore(redScore);
                }

                if(binColour == BinColour.Green)
                {
                    greenScore += 50;
                    SetGreenScore(greenScore);
                }

                if(binColour == BinColour.Blue)
                {
                    blueScore += 50;
                    SetBlueScore(blueScore);
                }

                if(binColour == BinColour.Yellow)
                {
                    yellowScore += 50;
                    SetYellowScore(yellowScore);
                }

                Invoke("Hidden", t);

                Debug.Log("Correct!");
            }
            else
            {
                AudioManager.Instance.PlaySFX("Incorrect", false);

                minusScore.SetActive(true);

                if (binColour == BinColour.Red)
                {
                    redScore -= 20;
                    SetRedScore(redScore);
                }

                if (binColour == BinColour.Green)
                {
                    greenScore -= 20;
                    SetGreenScore(greenScore);
                }

                if (binColour == BinColour.Blue)
                {
                    blueScore -= 20;
                    SetBlueScore(blueScore);
                }

                if (binColour == BinColour.Yellow)
                {
                    yellowScore -= 20;
                    SetYellowScore(yellowScore);
                }

                Invoke("Hidden", t);

                Debug.Log("Wrong!");
            }
        }
    }

    void Start()
    {
        addScore.SetActive(false);
        minusScore.SetActive(false);

        rbody = GetComponent<Rigidbody2D>();

        SetRedScore(0);
        SetBlueScore(0);
        SetGreenScore(0);
        SetYellowScore(0);
    }
}
