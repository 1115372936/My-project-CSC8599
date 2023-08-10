using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinControl : MonoBehaviour
{
    public enum BinColour
    {
        Red = 0,
        Yellow = 1,
        Green = 2,
        Blue = 3,
    }

    public BinColour binColour;

    Rigidbody2D rbody;

    public AudioClip correctClip;
    public AudioClip incorrectClip;

    void OnCollisionEnter2D(Collision2D other)
    {
        RubbishCollectable rubbish = other.gameObject.GetComponent<RubbishCollectable>();

        if (rubbish != null)
        {
            rubbish.Thrown();
            if(rubbish.belonging == binColour)
            {
                AudioManager.instance.AudioPlay(correctClip);
                Debug.Log("Correct!");
            }
            else
            {
                AudioManager.instance.AudioPlay(incorrectClip);
                Debug.Log("Wrong!");
            }
            //Debug.Log("Player throws the rubbish.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
