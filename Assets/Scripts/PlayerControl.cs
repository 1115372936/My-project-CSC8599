using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed =20;
    public GameObject ropePrefab;

    private Vector2 moveDirection = new Vector2(0, -1);

    Rigidbody2D rbody;
    Animator anim;

    void Start(){
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");

        float moveX = 0, moveY = 0;
        if (Input.GetKey(GameManager.GM.forward))
        {
            moveY = 1;
        }

        if (Input.GetKey(GameManager.GM.backward))
        {
            moveY = -1;
        }

        if (Input.GetKey(GameManager.GM.left))
        {
            moveX = -1;
        }

        if (Input.GetKey(GameManager.GM.right))
        {
            moveX = 1;
        }

        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveVector.x != 0 || moveVector.y != 0) {
            moveDirection = moveVector;
        }

        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);

        Vector2 position = rbody.position;
        position += moveVector * speed * Time.deltaTime;
        rbody.MovePosition(position);

        if (Input.GetKeyDown(GameManager.GM.collect))
        {
            AudioManager.Instance.PlaySFX("Shoot", false);
            GameObject rope = Instantiate(ropePrefab, rbody.position - Vector2.up * 0.75f, Quaternion.identity);
            RopeControl rc = rope.GetComponent<RopeControl>();
            if (rc != null)
            {
                rc.Move(moveDirection, 500);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bin"))
    //    {
    //        isNear = true;
    //    }
    //}
}
