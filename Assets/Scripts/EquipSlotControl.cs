using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipSlotControl : MonoBehaviour
{
    public enum objectType
    {
        none = 0,
        grass = 1,
        bone = 2,
        can = 3,
        banana = 4,
        battery = 5,
        plasticbag = 6,
        bottlecap = 7,
        cloth =8,
        bowl = 9,
        socks = 10,
        fishbone = 11,
        soccer = 12,
        oil = 13,
        bomb = 14,
        bulb = 15,
    }

    [System.Serializable]
    public struct Attribute
    {
        public objectType type;
        public int maxNum;
    }
    private Vector2 moveDirection = new Vector2(0, -1);

    public Transform father;
    public Transform player;

    public ItemControl itemObject;
    public int slotNumber = 2;

    public static List<ItemControl> items = new List<ItemControl>();
    public static List<bool> isFull = new List<bool>();

    public Attribute[] attributes;

    public static List<RubbishCollectable> collectable = new List<RubbishCollectable>();

    public RectTransform rect;
    public ItemControl itemControl;

    public void ChangeItemNumber(int i)
    {
        for(int fFor = 0; fFor < i; fFor++)
        {
            isFull.Add(false);
            items.Add(Instantiate(itemObject, father));
            items[items.Count - 1].gameObject.SetActive(true);
        }
    }

    public void SelectItem(ItemControl selectedObject)
    {
        int x = selectedObject.placedNumber;
        EquipSlotControl.objectType y = selectedObject.ot;
        selectedObject.placedNumber = itemControl.placedNumber;
        selectedObject.ot = itemControl.ot;
        isFull[items.IndexOf(selectedObject)] = false;
        itemControl.placedNumber = x;
        itemControl.ot = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeItemNumber(slotNumber);
    }

    // Update is called once per frame
    void Update()
    {
        //rect.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (items[0].placedNumber > 0)
            {
                GetComponent<Spawn>().SpawnDroppedItem();
                items[0].ot = objectType.none;
                items[0].placedNumber = 0;
                isFull[0] = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (items[1].placedNumber > 0)
            {
                GetComponent<Spawn>().SpawnDroppedItem();
                items[1].ot = objectType.none;
                items[1].placedNumber = 0;
                isFull[1] = false;
            }
        }
    }
}
