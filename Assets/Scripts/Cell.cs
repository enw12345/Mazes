using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int cellRow;
    public int cellColumn;
    private Vector2 cellPos;
    private bool isPosSet = false;
    private bool isIndexSet = false;
    private int cellIndex;

    public int CellIndex
    {
        get
        {
            if (!isIndexSet)
                SetIndex();
            return cellIndex;
        }
    }

    public Vector2 CellPos
    {
        get
        {
            if (!isPosSet)
                SetPos();
            return cellPos;
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

    private void SetPos()
    {
        cellPos = new Vector2(cellColumn, cellRow);
        isIndexSet = true;
    }

    private void SetIndex()
    {
        cellIndex = Grid.cellCountX * cellColumn + cellRow;
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
