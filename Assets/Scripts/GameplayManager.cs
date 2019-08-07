using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static int gridwidth = 10;
    public static int gridheight = 20;

    public static Transform[,] grid = new Transform[gridwidth, gridheight];

    private void Start()
    {
        GenerateTetromino();
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

    public Transform GetTransformAtGridPosition(Vector3 pos)
    {
        if (pos.y > gridwidth - 1)
            return null;
        else
            return grid[(int)pos.x, (int)pos.y];
    }

    public void UpdateGrid(TetrominoHandler tetromino)
    {
        for (int y = 0; y< gridwidth; y++)
        {
            for(int x=0;x<gridwidth; x++)
            {
                if(grid[x,y]!=null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                        grid[x, y] = null;
                }
            }
        }

        foreach (Transform mino in tetromino.transform)
        {
            Vector3 pos = Round(mino.position);

            if (pos.y < gridwidth)
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
            (int)pos.x < gridwidth &&
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
