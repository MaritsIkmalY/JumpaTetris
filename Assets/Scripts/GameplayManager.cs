using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    private void Start()
    {
        GenerateTetromino();
    }

    private bool IsRowFullAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] == null)
                return false;
        }

        return true;
    }

    private void DestroyRowAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    private void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += Vector3.down;
            }
        }
    }

    private void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; i++)
             MoveRowDown(i);        
    }
    private string GetRdandomTetromino()
    {
        int val = Random.Range(0, 7);
        string tetrominoName = "TetrominoT";

        switch (val)
        {
            case 0:
                tetrominoName = "TetrominoI";
                break;
            case 1:
                tetrominoName = "TetrominoJ";
                break;
            case 2:
                tetrominoName = "TetrominoL";
                break;
            case 3:
                tetrominoName = "TetrominoT";
                break;
            case 4:
                tetrominoName = "TetrominoO";
                break;
            case 5:
                tetrominoName = "TetrominoS";
                break;
            case 6:
                tetrominoName = "TetrominoZ";
                break;
        }

        return "Prefabs/" + tetrominoName;
    }

    public void DestroyRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if(IsRowFullAt(y))
            {
                DestroyRowAt(y);

                MoveAllRowsDown(y + 1);

                y--;
            }
        }
    }

    public bool IsReactLimitGrid(TetrominoHandler tetromino)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector3 pos = Round(mino.position);

                if(pos.y >= gridHeight -1 &&
                    !tetromino.isActiveAndEnabled)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void GameOver(TetrominoHandler tetromino)
    {
        Debug.Log("Game is Over!");

        enabled = false;
    }
    public Transform GetTransformAtGridPosition(Vector3 pos)
    {
        if (pos.y > gridHeight - 1)
            return null;
        else
            return grid[(int)pos.x, (int)pos.y];
    }

    public void UpdateGrid(TetrominoHandler tetromino)
    {
        for (int y = 0; y< gridHeight; y++)
        {
            for(int x=0;x<gridWidth; x++)
            {
                if(grid[x,y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                        grid[x, y] = null;
                }
            }
        }

        foreach (Transform mino in tetromino.transform)
        {
            Vector3 pos = Round(mino.position);

            if (pos.y < gridHeight)
                grid[(int)pos.x, (int)pos.y] = mino;
        }
    }

    public void GenerateTetromino()
    {
        GameObject tetromino = (GameObject)Instantiate(Resources.Load(GetRdandomTetromino(), typeof(GameObject)),
                                                       new Vector3(5.0f, 18.0f, 0.0f),
                                                       Quaternion.identity);
    }

    public bool IsTetrominoIndisideGrid(Vector3 pos)
    {
        return (
            (int)pos.x >= 0 &&
            (int)pos.x < gridWidth &&
            (int)pos.y >= 0
        );
    }
    public Vector3 Round(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y),
            Mathf.Round(pos.z)
            );
    }
}
