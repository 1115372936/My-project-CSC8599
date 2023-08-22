using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject collectionMenu;
    public GameObject settingMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SettingGame()
    {
        settingMenu.SetActive(true);
    }

    public void CloseSetting()
    {
        settingMenu.SetActive(false);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenCollection()
    {
        collectionMenu.SetActive(true);
    }

    public void CloseCollection()
    {
        collectionMenu.SetActive(false);
    }
}
