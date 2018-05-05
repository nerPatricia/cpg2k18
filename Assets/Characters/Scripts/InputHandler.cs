using System;
using UnityEngine;


public class InputHandler {

    public static void HandleInput(Character player) {

        if (Input.GetAxis("Horizontal") == 1) {
            Debug.Log("Moving to right");
            player.MoveRight();
        }
        else if (Input.GetAxis("Horizontal") == -1) {
            player.MoveLeft();
        }
    }
}
