using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class RopeControl : MonoBehaviour
{
    Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

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
            c.Collected();
            Debug.Log("The rubbish has been collected.");
        }
        Destroy(this.gameObject);
    }
}
