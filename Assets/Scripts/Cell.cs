using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int cellRow;
    public int cellColumn;
    private Vector2 cellIndex;
    private bool isIndexSet = false;

    public Vector2 CellIndex
    {
        get
        {
            if (!isIndexSet)
                SetIndex();
            return cellIndex;
        }
    }

    public GameObject northWall, southWall, eastWall, westWall;

    private GameObject[] walls;
    private bool areWallsSet = false;

    public GameObject[] Walls
    {
        get
        {
            if (!areWallsSet)
                SetWalls();
            return walls;
        }
    }

    public bool visited = false;

    public static List<Cell> Maze = new List<Cell>();

    private void SetIndex()
    {
        cellIndex = new Vector2(cellColumn, cellRow);
        isIndexSet = true;
    }

    private void SetWalls()
    {
        if (southWall != null && westWall != null)
            walls = new GameObject[] { northWall, eastWall, southWall, westWall };
        else if (southWall != null)
            walls = new GameObject[] { northWall, eastWall, southWall };
        else if (westWall != null)
            walls = new GameObject[] { northWall, eastWall, westWall };
        else
            walls = new GameObject[] { northWall, eastWall };

        areWallsSet = true;
    }

}
