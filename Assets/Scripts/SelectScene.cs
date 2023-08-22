using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public enum Level
    {
        simple = 0, middle = 1, hard = 2, super = 3,
    }

    public Level level;

    void LeadToScene()
    {
        if (level == Level.simple)
        {
            SceneManager.LoadScene(2);
        }

        else if (level == Level.middle)
        {
            SceneManager.LoadScene(3);
        }

        else if (level == Level.hard)
        {
            SceneManager.LoadScene(4);
        }

        else
        {
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();

        if (pc != null)
        {
            AudioManager.Instance.PlaySFX("GetCoin", false);

            LeadToScene();

            Destroy(gameObject);
        }
    }
}
