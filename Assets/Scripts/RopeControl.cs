using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class RopeControl : MonoBehaviour
{
    Rigidbody2D rbody;

    //public AudioClip hitClip;

    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubbishCollectable c = other.gameObject.GetComponent<RubbishCollectable>();

        if (c != null)
        {
            if (EquipSlotControl.isFull.Contains(false))
            {
                for (int i = 0; i < EquipSlotControl.items.Count; i++)
                {
                    EquipSlotControl.collectable.Add(c);
                    if (!EquipSlotControl.isFull[i])
                    {
                        if (EquipSlotControl.items[i].ot == EquipSlotControl.objectType.none)
                        {
                            EquipSlotControl.items[i].ot = c.type;
                            EquipSlotControl.items[i].placedNumber += c.num;

                            EquipSlotControl.isFull[i] = EquipSlotControl.items[i].placedNumber >= 1;

                            break;
                        }
                        else if (EquipSlotControl.items[i].ot == c.type)
                        {
                            EquipSlotControl.items[i].placedNumber += c.num;

                            EquipSlotControl.isFull[i] = EquipSlotControl.items[i].placedNumber >= 1;

                            break;
                        }
                    }
                }
                c.Collected();
                Debug.Log("The rubbish has been collected.");
            }
            else
            {
                Debug.Log("There's no space.");
            }
        }

        //AudioManager.instance.AudioPlay(hitClip);
        Destroy(this.gameObject);
    }
}
