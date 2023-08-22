using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode collect { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set;  }

    private void Awake()
    {
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }

        collect = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("collectKey", "Space"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
