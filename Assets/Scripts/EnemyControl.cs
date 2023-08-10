using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed = 10;

    public bool isVertical;

    public float changeDirectionTime = 2f;

    private float changeTimer;

    private Vector2 moveDirection;

    private Rigidbody2D rbody;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveDirection = isVertical ? Vector2.up : Vector2.right;

        changeTimer = changeDirectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        changeTimer -= Time.deltaTime;
        if(changeTimer < 0)
        {
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
        }

        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;

        rbody.MovePosition(position);
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();
        if(pc != null)
        {
            pc.UseHand(-1);
        }
    }
}
