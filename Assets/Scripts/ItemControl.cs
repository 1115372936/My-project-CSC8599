using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    public Image item;
    public EquipSlotControl.objectType ot;

    public int placedNumber = 0;

    public Text numberText;
    //public Text keyboardText;

    public Sprite[] itemImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        item.gameObject.SetActive(placedNumber > 0);
        item.sprite = itemImage[(int)ot];
        numberText.text = placedNumber > 1 ? placedNumber.ToString() : "";
    }
}
