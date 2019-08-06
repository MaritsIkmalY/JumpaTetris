using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoHandler : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 1.0f;

    private float fall = 0.0f;
    private void Update()
    {
        UpdateTetromino();
        InputKeyboardHandler();
    }

    private void UpdateTetromino()
    {
        if (Time.time - fall >= fallSpeed)
        {
            Handler("Down");
            fall = Time.time;
        }
    }

    private void InputKeyboardHandler()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Handler("Right");
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Handler("Left");
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Handler("Down");
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            Handler("Action");
    }

    private void Handler(string command)
    {
        switch (command)
        {
            case "Right":
                transform.position += Vector3.right;
                break;
            case "Left":
                transform.position += Vector3.left;
                break;
            case "Down":
                transform.position += Vector3.down;
                break;
            case "Action":
                transform.Rotate(Vector3.forward * 90);
                break;

        }
    }
}
