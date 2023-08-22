using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{

    private Transform settingPanel;
    private Event keyEvent;
    private Text buttonText;
    private KeyCode newKey;

    private bool waitingForKey;

    private void Start()
    {
        settingPanel = transform.Find("Key");

        waitingForKey = false;

        for (int i = 0; i < settingPanel.childCount; i++)
        {
            if (settingPanel.GetChild(i).name == "ForwardBtn")
            {
                settingPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.forward.ToString();
            }
            else if (settingPanel.GetChild(i).name == "BackwardBtn")
            {
                settingPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.backward.ToString();
            }
            else if (settingPanel.GetChild(i).name == "LeftwardBtn")
            {
                settingPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();
            }
            else if (settingPanel.GetChild(i).name == "RightwardBtn")
            {
                settingPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();
            }
            else if (settingPanel.GetChild(i).name == "RopeBtn")
            {
                settingPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.collect.ToString();
            }
        }
    }

    private void OnGUI()
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StarAssignment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "forward": GameManager.GM.forward = newKey;
                buttonText.text = GameManager.GM.forward.ToString();
                PlayerPrefs.SetString("forwardKey", GameManager.GM.forward.ToString());
                break;
            case "backward":
                GameManager.GM.backward = newKey;
                buttonText.text = GameManager.GM.backward.ToString();
                PlayerPrefs.SetString("backwardKey", GameManager.GM.backward.ToString());
                break;
            case "left":
                GameManager.GM.left = newKey;
                buttonText.text = GameManager.GM.left.ToString();
                PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString());
                break;
            case "right":
                GameManager.GM.right = newKey;
                buttonText.text = GameManager.GM.right.ToString();
                PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString());
                break;
            case "collect":
                GameManager.GM.collect = newKey;
                buttonText.text = GameManager.GM.collect.ToString();
                PlayerPrefs.SetString("collectKey", GameManager.GM.collect.ToString());
                break;
        }

        yield return null;
    }
}
