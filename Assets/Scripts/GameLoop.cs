using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour {

    public Character player;

    public Transform[] groundLayers;
    public GameObject lifeIcon;
    public GameObject lifePanel;

    public SpriteRenderer[] bombicons;

    public Sprite bombIcon;
    public Sprite usedBombIcon;

    public int hudLife;

    public GameObject ThrowPowerHUD;
    public RectTransform powerThrowRender;

    void Start() {
		player = GameObject.FindWithTag ("Player").GetComponent<Character>();
		lifePanel = GameObject.FindWithTag ("HUD");
        hudLife = player.maxLife;
        for (int i = 0; i< hudLife; i++) {
            Instantiate(lifeIcon, lifePanel.transform);
        }
    }

    void Update() {
        InputHandler.HandleInput(player);
        UpdateLifeHUD();
        UpdateBombHUD();
        UpdateThrowBombHUD();
    }


    private void UpdateLifeHUD() {
        if (this.hudLife  > player.life) {
            this.hudLife--;
            Transform[] lifeIcons = this.lifePanel.GetComponentsInChildren<Transform>();
            Debug.Log(lifeIcons[1].name);
            Destroy(lifeIcons[1].gameObject);
        }
        else if (this.hudLife < player.life) {
            this.hudLife++;
            Instantiate(lifeIcon,lifePanel.transform);
        }
    }

    private void UpdateBombHUD() {
        for (int i = 0; i < 5; i++) {
            if (i < player.bombsLeft) {
                bombicons[i].sprite = bombIcon;
            }
            else {
                bombicons[i].sprite = usedBombIcon;
            }
        }
    }

    private void UpdateThrowBombHUD() {
        if (player.charging) {
            ThrowPowerHUD.SetActive(true);
            ThrowPowerHUD.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 2);
            float x = player.attackDistance / player.maxAttackDistance;
            powerThrowRender.localScale = new Vector3(x, powerThrowRender.transform.localScale.y, powerThrowRender.transform.localScale.z);
        }
        else {
            ThrowPowerHUD.SetActive(false);
        }
        
    }


    private void GameOver() {

    }

    private void Menu() {

    }
}