using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public enum EnemyType
    {
        CanThrow = 0,
        CanKick = 1,
        CanBlock = 2,
    }
    public EnemyType enemyType;

    public float speed;
    public bool isVertical;

    public float changeDirectionTime;
    private float changeTimer;

    private Vector2 moveDirection;

    private Rigidbody2D rbody;
    private Animator anim;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveDirection = isVertical ? Vector2.up : Vector2.right;
        changeTimer = changeDirectionTime;
    }

    void Update()
    {
        if(enemyType == EnemyType.CanThrow)
        {
            changeTimer -= Time.deltaTime;
            if (changeTimer < 0)
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

        if(enemyType == EnemyType.CanKick)
        {
            changeTimer -= Time.deltaTime;
            if (changeTimer < 0)
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
    }
}
