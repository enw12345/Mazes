using UnityEngine;
using System.Collections;
using System.Linq;

public static class Maze
{
    private const int cellCountX = 4;
    private const int cellCountY = 4;
    private const float offset = 0;

    private static int wallCount;
    private static GameObject[] walls;

    private static Vector3 horizontalRot = new Vector3(0, 90, 0);

    public static Vector3[,] Grid = new Vector3[cellCountX, cellCountY];

    private static bool mazeCreated = false;

    /// <summary>
    /// Create grid coordinates
    /// </summary>
    /// <param name="xSize"></param>
    /// <param name="ySize"></param>
    /// <param name="offset"></param>
    private static void CreateGrid(int xSize, int ySize, float offset)
    {
        Vector3 position = Vector3.zero;

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                position.x = (i + offset);
                position.z = (j + offset);
                position.y = .5f;

                Grid[i, j] = position;
            }
        }
    }

    public static void CreateGridLayout(Transform maze, GameObject wall, Bounds wallBounds, int xSize = cellCountX, int ySize = cellCountY, float offset = offset)
    {
        if (mazeCreated == false)
        {
            Grid = new Vector3[xSize, ySize];
            Quaternion wallRot = Quaternion.Euler(horizontalRot);

            CreateGrid(xSize, ySize, offset);

            wallCount = (xSize * xSize + xSize) + (ySize * ySize + xSize);
            walls = new GameObject[wallCount];

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Vector3 positionVert;
                    Vector3 positionHorz;

                    positionHorz = new Vector3(Grid[i, j].x, Grid[i, j].y, Grid[i, j].z - wallBounds.extents.z);
                    positionVert = new Vector3(Grid[i, j].x - wallBounds.extents.z, Grid[i, j].y, Grid[i, j].z);

                    walls[i * j] = GameObject.Instantiate(wall, positionHorz, wallRot, maze);
                    walls[i * j] = GameObject.Instantiate(wall, positionVert, Quaternion.identity, maze);

                    if (j == ySize - 1)
                    {
                        Vector3 positionHorzBottom = new Vector3(Grid[i, j].x, Grid[i, j].y, Grid[i, j].z + wallBounds.extents.z);
                        walls[i * j] = GameObject.Instantiate(wall, positionHorzBottom, wallRot, maze);
                    }

                    if (i == xSize - 1)
                    {
                        Vector3 positionVertSide = new Vector3(Grid[i, j].x + wallBounds.extents.z, Grid[i, j].y, Grid[i, j].z);
                        walls[i * j] = GameObject.Instantiate(wall, positionVertSide, Quaternion.identity, maze);
                    }
                }
            }
        }

        mazeCreated = true;
    }
}

public class Cell
{
    public int cellRow;
    public int cellColumn;
    public int cellIndex;
    public bool visited;
}
