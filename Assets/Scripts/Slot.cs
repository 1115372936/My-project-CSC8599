using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;

    public int i;

    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);

            if (UIManager.GetIsStep5())
            {
                NoviceGuidePanel.instance.NextStep(MenuGuideConst.ClickBtnStep5);

                UIManager.SetIsStep5(false);
            }
        }
    }

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
}
