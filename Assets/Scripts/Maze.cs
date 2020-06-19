using UnityEngine;
using System.Collections;

public static class Maze
{
    private const int cellCountX = 4;
    private const int cellCountY = 4;
    private const float top = 0;

    private static int wallCount;
    private static GameObject[] walls;

    private static Vector3 horizontalRot = new Vector3(0, 90, 0);
    private static Quaternion wallRot;

    public static Vector3[,] Grid = new Vector3[cellCountX, cellCountY];

    private static void CreateGrid(int xSize, int ySize, Bounds wallBounds, float top)
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
        CreateGrid(xSize, ySize, wallBounds, top);
        wallRot.eulerAngles = horizontalRot;

        wallCount = ((cellCountX - 1) * cellCountX) + ((cellCountY - 1) * cellCountY);
        walls = new GameObject[wallCount];

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                Vector3 positionVert;
                Vector3 positionHorz;

                positionVert = new Vector3(Grid[i, j].x - wallBounds.size.z * .5f, Grid[i, j].y, Grid[i, j].z);
                positionHorz = new Vector3(Grid[i, j].x, Grid[i, j].y, Grid[i, j].z - wallBounds.size.z * .5f);

                if (j != ySize - 1)
                    walls[i * j] = GameObject.Instantiate(wall, positionVert, Quaternion.identity, maze);
                if (i != xSize - 1)
                    walls[i * j] = GameObject.Instantiate(wall, positionHorz, wallRot, maze);
            }
        }
    }
}
