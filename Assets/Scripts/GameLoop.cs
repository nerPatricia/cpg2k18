using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {

    public Character player;

    public Transform[] groundLayers;
    public GameObject lifeIcon;
    public GameObject lifePanel;
    public int hudLife;
    void Start() {
        hudLife = player.maxLife;
        for (int i = 0; i< hudLife; i++) {
            Instantiate(lifeIcon, lifePanel.transform);
        }
    }

    void Update() {
        InputHandler.HandleInput(player);

    }


    private void UpdateLifeHUD() {
        if (this.hudLife  > player.life) {
            this.hudLife--;
            Transform lifeIcons = this.lifePanel.GetComponentInChildren<Transform>();
        }
        else {
            this.hudLife++;
            Instantiate(lifeIcon,lifePanel.transform);
        }
    }
}