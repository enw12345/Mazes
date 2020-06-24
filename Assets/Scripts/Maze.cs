using UnityEngine;
using System.Collections;
using System.Linq;

public static class Maze
{
    private const int cellCountX = 4;
    private const int cellCountY = 4;
    private const float top = 0;

    private static int wallCount;
    private static GameObject[] walls;

    private static Vector3 horzTopRot = new Vector3(0, 90, 0);
    //private static Vector3 horzBottomRot = new Vector3(0, 90, 0);
    private static Quaternion wallRot;

    public static Vector3[,] Grid = new Vector3[cellCountX, cellCountY];

    private static void CreateGrid(int xSize, int ySize, float top)
    {
        Vector3 position = Vector3.zero;

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                position.x = (i + top);
                position.z = (j + top);
                position.y = .5f;

                Grid[i, j] = position;
            }
        }
    }

    public static void CreateGridLayout(Transform maze, GameObject wall, Bounds wallBounds, int xSize = cellCountX, int ySize = cellCountY, float top = top)
    {
        Grid = new Vector3[xSize, ySize];
        CreateGrid(xSize, ySize, top);
        wallRot.eulerAngles = horzTopRot;

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

                if(j == ySize - 1)
                {
                    Vector3 positionHorzBottom = new Vector3(Grid[i, j].x, Grid[i, j].y, Grid[i, j].z + wallBounds.extents.z);
                    walls[i * j] = GameObject.Instantiate(wall, positionHorzBottom, wallRot, maze);
                }

                if(i == xSize - 1)
                {
                    Vector3 positionVertSide = new Vector3(Grid[i, j].x + wallBounds.extents.z, Grid[i, j].y, Grid[i, j].z);
                    walls[i * j] = GameObject.Instantiate(wall, positionVertSide, Quaternion.identity, maze);
                }
            }
        }
    }
}
