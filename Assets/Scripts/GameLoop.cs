using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int speed;


    private Rigidbody2D RB2d;
    private Transform characterTransform;
    private BoxCollider2D BC2d;
    private Animator animator;




    // Use this for initialization
    void Start() {
        this.RB2d = this.GetComponent<Rigidbody2D>();
        this.characterTransform = this.transform;
        this.animator = this.GetComponent<Animator>();
        this.BC2d = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate() {
        
    }



    public void Mirror(int dir) {
        if (dir == 6) characterTransform.rotation = new Quaternion(0, 0, 0, 0);
        if (dir == 4) characterTransform.rotation = new Quaternion(0, 180, 0, 0);
    }

    public void MoveLeft() {
        this.Mirror(4);
        this.Move();
    }

    public void MoveRight() {
        this.Mirror(6);
        this.Move();
    }

    public void Move() {
        this.animator.SetBool("walk", true);
        this.transform.Translate(new Vector2(1,0)*Time.deltaTime * this.speed);

    }
}

