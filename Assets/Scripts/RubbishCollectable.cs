using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RubbishCollectable : MonoBehaviour
{
    public BinControl.BinColour belonging;

    private Inventory inventory;
    public GameObject itemButton;

    public ParticleSystem collectEffect;

    public float speed;

    public Transform goalPos;

    private Vector2 StartPos;
    private Vector2 EndPos;

    private bool isMove;

    public static bool isCollect = false;

    private void Start()
    {
        StartPos = this.transform.position;
        EndPos = goalPos.position;

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        if (!isMove)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, EndPos, step);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Collected()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                isCollect = true;

                Instantiate(itemButton, inventory.slots[i].transform, false);
                inventory.isFull[i] = true;

                Instantiate(collectEffect, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySFX("Collect", false);

                Destroy(gameObject);
                break;
            }
        }
    }

    public void Thrown()
    {
        Destroy(this.gameObject);
    }
}
