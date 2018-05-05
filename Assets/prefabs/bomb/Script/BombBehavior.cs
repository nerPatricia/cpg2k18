using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

	public float minY;
	public float timeToExplode;
	public Character playerStatus;
	private RaycastHit2D[] foesHit;

	// Use this for initialization
	void Start () {
        StartCoroutine("Timer");
	}

	void Update () {
        bool stopped = false;
        if (!stopped && this.transform.position.y < this.minY)
        {
            this.GetComponent<Rigidbody2D> ().gravityScale = 0;
            this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
            stopped = true;
        }
    }

    IEnumerator Timer() {
        for (float i = 0; i < timeToExplode; i+=.1f) {
            yield return new WaitForSeconds(.1f);
        }
		Explode ();
    }

    private void Explode() {
        //chama a animação de explosão
        //raycast nos inimigos ao redor que estão na mesma camada
        //chama a função de dano neles
        Destroy(this.gameObject);
		foesHit = Physics2D.CircleCastAll (this.transform.position, 2.0f, new Vector2(1,1), 2.0f);
		for (int i = 0; i < foesHit.Length; ++i) {
		//	
			if (foesHit [i].collider != null && foesHit[i].collider.name != "Player") {
				Debug.Log (foesHit [i].collider.name);
				foesHit [i].collider.gameObject.GetComponent<Foe>().Damage (1);
			}
		}
		playerStatus.bombsLeft++;

    }
}
