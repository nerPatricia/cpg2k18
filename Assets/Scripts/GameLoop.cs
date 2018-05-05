using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {

    public Character player;

    public Transform[] groundLayers;

    void Start() {

    }

    void Update() {
        InputHandler.HandleInput(player);

    }
}