using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

    public float timeToExplode;

	// Use this for initialization
	void Start () {
        StartCoroutine("Timer");
	}

    IEnumerator Timer() {
        for (float i = 0; i < timeToExplode; i+=.1f) {
            yield return new WaitForSeconds(.1f);
            Debug.Log("olá");
        }
    }

    private void Explode() {
        //chama a animação de explosão
        //raycast nos inimigos ao redor que estão na mesma camada
        //chama a função de dano neles
        Destroy(this.gameObject);
    }
}
