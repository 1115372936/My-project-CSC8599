using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed = 5f;
    public GameObject ropePrefab;

    public AudioClip shootClip;

    private int maxHand = 2;
    private int currentHand;

    private Vector2 moveDirection = new Vector2(0, -1);

    public int MyMaxHand { get { return maxHand; } }
    public int MyCurrentHand { get { return currentHand; } }

    Rigidbody2D rbody;
    Animator anim;

    // Start is called before the first frame update
    void Start(){
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHand = 0;
    }

    // Update is called once per frame
    void Update(){
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveVector.x != 0 || moveVector.y != 0) {
            moveDirection = moveVector;
        }

        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);

        Vector2 position = rbody.position;
        position += moveVector * speed * Time.deltaTime;
        rbody.MovePosition(position);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.AudioPlay(shootClip);
            GameObject rope = Instantiate(ropePrefab, rbody.position - Vector2.up * 0.75f, Quaternion.identity);
            RopeControl rc = rope.GetComponent<RopeControl>();
            if (rc != null)
            {
                rc.Move(moveDirection, 500);
            }
        }

    }

    public void UseHand(int amount)
    {
        Debug.Log(currentHand + "/" + maxHand);
        currentHand = Mathf.Clamp(currentHand + amount, 0, maxHand);
        Debug.Log(currentHand + "/" + maxHand);
    }
}
