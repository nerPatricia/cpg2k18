using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int maxLife;
    public int life;

    public int speed;
    public GameLoop gameLoop;
    public int groundIndex = 0;

    private Rigidbody2D RB2d;
    private BoxCollider2D BC2d;
    private Animator animator;


    public float verticalUpdateDistance = 0.5f;


    public float attackDistance;
    public float maxAttackDistance;

    public float timeToBombExplode;

    public GameObject bombPrefab;
    public GameObject superBombPrefab;
	public int maxBombs = 5;
	public int bombsLeft;


    public Text timer;

    // Use this for initialization
    void Start() {

        this.RB2d = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.BC2d = this.GetComponent<BoxCollider2D>();

        this.transform.position = new Vector2(this.transform.position.x, gameLoop.groundLayers[this.groundIndex].position.y);

        this.life = maxLife;
		this.bombsLeft = maxBombs;
        StartCoroutine("TimerToExplode");
    }

    // Update is called once per frame
    void Update() {
        this.timer.transform.position = new Vector2(this.transform.position.x, (this.transform.position.y+1.7f));

    }

    void FixedUpdate() {

    }



    public void Mirror(int dir) {
        if (dir == 6) this.transform.rotation = new Quaternion(0, 0, 0, 0);
        if (dir == 4) this.transform.rotation = new Quaternion(0, 180, 0, 0);
    }

    public void MoveLeft() {
        this.Mirror(4);
        this.Move();
    }

    public void MoveRight() {
        this.Mirror(6);
        this.Move();
    }

    public void MoveDown() {
        if (groundIndex > 0) {
            groundIndex--;
            StopCoroutine("VerticalMove");
            StartCoroutine("VerticalMove",2);
        } 
    }

    public void MoveUp() {
        if (groundIndex < (gameLoop.groundLayers.Length - 1)) {
            groundIndex++;
            StopCoroutine("VerticalMove");
            StartCoroutine("VerticalMove",8);
        }
    }

    IEnumerator VerticalMove(int dir) {
        this.animator.SetBool("walking", true);

        float distance = Mathf.Abs(this.transform.position.y - gameLoop.groundLayers[this.groundIndex].position.y);

        for(float i = 0; i < distance; i+= verticalUpdateDistance) {
            if (dir == 2) this.transform.Translate(new Vector2(0, -verticalUpdateDistance)); 
            if (dir == 8) this.transform.Translate(new Vector2(0, verticalUpdateDistance));
            yield return new WaitForSeconds(.001f);
        }
    }

    public void Move() {
        this.animator.SetBool("walking", true);
        this.transform.Translate(new Vector2(1, 0) * Time.deltaTime * this.speed);
    }

    public void StopWalking() {
        this.animator.SetBool("walking", false);
    }

    public void BeginChargeAttack() {
        StartCoroutine("ChargingAttack");
    }

    IEnumerator ChargingAttack() {
        attackDistance = 0;
        while (true) {
            if (attackDistance > maxAttackDistance) {
                attackDistance = 0;
            }
            else {
                attackDistance += 1;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    public void Attack() {
        StopCoroutine("ChargingAttack");
		if (bombsLeft > 0) {
			this.animator.SetTrigger("attack");
			Rigidbody2D projectile = bombPrefab.GetComponent<Rigidbody2D> ();
			Rigidbody2D clone;
			bombsLeft--;
			clone = Instantiate (projectile, this.transform.position, Quaternion.identity) as Rigidbody2D;
			clone.GetComponent<BombBehavior> ().minY = this.transform.position.y - 1;
			clone.GetComponent<BombBehavior> ().playerStatus = this;
			clone.velocity = transform.TransformDirection ((Vector2.right + (2 * Vector2.up)) * attackDistance);
			RestartTimerToExplode();	
		}
    }

    public void Super() {
        this.animator.SetTrigger("super");
        RestartTimerToExplode();
    }

    IEnumerator TimerToExplode() {
        float leftTime = timeToBombExplode;
        while (leftTime > 0) {
            leftTime--;
            this.timer.text = leftTime+"/" + timeToBombExplode;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Explodiu");
        this.animator.SetTrigger("bombTimeOut");
        Damage(1);

        RestartTimerToExplode();
    }

    private void RestartTimerToExplode() {
        StopCoroutine("TimerToExplode");
        StartCoroutine("TimerToExplode");
    }

    private void Damage(int damage) {
        this.life -= damage;
    }
}

