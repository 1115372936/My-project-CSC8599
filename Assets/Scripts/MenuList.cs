using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;

    private bool key = true;

    void Update()
    {
        if (key)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu.SetActive(true);
                key = !key;
                Time.timeScale = 0;
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
            key = !key;
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        menu.SetActive(false);
        key = !key;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Help()
    {
        setting.SetActive(true);
        Time.timeScale = 0;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Close()
    {
        setting.SetActive(false);
        Time.timeScale = 0;
    }
}
