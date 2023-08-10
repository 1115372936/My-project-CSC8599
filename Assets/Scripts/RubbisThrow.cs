using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbisThrow : MonoBehaviour
{
    public Vector2 startSpeed;

    private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        //rbody.velocity = transform.up * startSpeed.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
