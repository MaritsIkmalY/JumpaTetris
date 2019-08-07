using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoHandler : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 0.5f;

    [SerializeField]
    private bool allowRotation = true;

    [SerializeField]
    private bool limitRotation = false;

    private float fall = 0.0f;

    private GameplayManager gameplayManager;
    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
    }
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
                MoveHorizontal(Vector3.right);
                break;
            case "Left":
                MoveHorizontal(Vector3.left);
                break;
            case "Down":
                MoveVertical();
                break;
            case "Action":
                if (allowRotation)
                {
                    ActionLimitRotation(1);

                    if (!IsInvalidPosition())
                        ActionLimitRotation(-1);
                    else
                        gameplayManager.UpdateGrid(this);
                }
                break;
        }
    }

    private void ActionLimitRotation(int modifier)
    {
        if (limitRotation)
        {
            if (transform.rotation.eulerAngles.z >= 90)
                transform.Rotate(Vector3.forward * -90);
            else
                transform.Rotate(Vector3.forward * 90);
        }
        else
            transform.Rotate(Vector3.forward * 90 * modifier);
    }
    private void MoveVertical()
    {
        transform.position += Vector3.down;
        if (!IsInvalidPosition())
        {
            transform.position += Vector3.up;

            gameplayManager.DestroyRow();

            enabled = false;

            gameplayManager.GenerateTetromino();
        }
        else
            gameplayManager.UpdateGrid(this);
    }
    private void MoveHorizontal(Vector3 direction)
    {
        transform.position += direction;

        if (!IsInvalidPosition())
            transform.position += direction * -1;
        else
            gameplayManager.UpdateGrid(this);
    }
    private bool IsInvalidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector3 pos = gameplayManager.Round(mino.position);

            if (!gameplayManager.IsTetrominoIndisideGrid(pos))
                return false;

            if (gameplayManager.GetTransformAtGridPosition(pos) != null &&
                gameplayManager.GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
