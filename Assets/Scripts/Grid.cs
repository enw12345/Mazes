using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class Grid
{
    public static  int cellCountX = 2;
    public static int cellCountY = 2;

    private static Vector3 horizontalRot = new Vector3(0, 90, 0);

    public static Vector3[,] GridContainer = new Vector3[cellCountX, cellCountY];

    private static bool mazeCreated = false;

    private static void CreateGridPositions(int xSize, int ySize, float offsetX, float offsetZ)
    {
        Vector3 position = Vector3.zero;

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                position.x = (i - offsetX);
                position.z = (j - offsetZ);
                position.y = .5f;

                GridContainer[i, j] = position;
            }
        }
    }

    public static void CreateGrid(Transform wallContainer, GameObject wall, Bounds wallBounds, Vector3 planeCenter, int xSize, int ySize)
    {
        if (!mazeCreated)
        {
            cellCountX = xSize;
            cellCountY = ySize;

            GridContainer = new Vector3[cellCountX, cellCountY];
            Quaternion wallRot = Quaternion.Euler(horizontalRot);

            float offsetX = ((xSize - 1) * wallBounds.extents.z) - planeCenter.x;
            float offsetZ = ((ySize - 1) * wallBounds.extents.z) - planeCenter.z;

            CreateGridPositions(xSize, ySize, offsetX, offsetZ);

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Cell cell = new Cell();

                    Vector3 positionVert;
                    Vector3 positionHorz;

                    positionHorz = new Vector3(GridContainer[i, j].x, GridContainer[i, j].y, GridContainer[i, j].z - wallBounds.extents.z);
                    positionVert = new Vector3(GridContainer[i, j].x - wallBounds.extents.z, GridContainer[i, j].y, GridContainer[i, j].z);

                    cell.northWall = GameObject.Instantiate(wall, positionHorz, wallRot, wallContainer);

                    cell.eastWall = GameObject.Instantiate(wall, positionVert, Quaternion.identity, wallContainer);

                    if (j == ySize - 1)
                    {
                        Vector3 positionHorzBottom = new Vector3(GridContainer[i, j].x, GridContainer[i, j].y, GridContainer[i, j].z + wallBounds.extents.z);
                        cell.southWall = GameObject.Instantiate(wall, positionHorzBottom, wallRot, wallContainer);
                    }

                    if (i == xSize - 1)
                    {
                        Vector3 positionVertSide = new Vector3(GridContainer[i, j].x + wallBounds.extents.z, GridContainer[i, j].y, GridContainer[i, j].z);
                        cell.westWall = GameObject.Instantiate(wall, positionVertSide, Quaternion.identity, wallContainer);
                    }

                    cell.cellRow = j;
                    cell.cellColumn = i;

                    Cell.Maze.Add(cell);
                }
            }
            mazeCreated = true;
        }
    }

    public static IEnumerator CreateGridStep(Transform wallContainer, GameObject wall, Bounds wallBounds, Vector3 planeCenter, float stepSpeed, int xSize, int ySize)
    {
        if (!mazeCreated)
        {
            cellCountX = xSize;
            cellCountY = ySize;

            GridContainer = new Vector3[cellCountX, cellCountY];
            Quaternion wallRot = Quaternion.Euler(horizontalRot);

            float offsetX = ((xSize - 1) * wallBounds.extents.z) - planeCenter.x;
            float offsetZ = ((ySize - 1) * wallBounds.extents.z) - planeCenter.z;

            CreateGridPositions(xSize, ySize, offsetX, offsetZ);

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Cell cell = new Cell();

                    Vector3 positionVert;
                    Vector3 positionHorz;

                    positionHorz = new Vector3(GridContainer[i, j].x, GridContainer[i, j].y, GridContainer[i, j].z - wallBounds.extents.z);
                    positionVert = new Vector3(GridContainer[i, j].x - wallBounds.extents.z, GridContainer[i, j].y, GridContainer[i, j].z);

                    cell.northWall = GameObject.Instantiate(wall, positionHorz, wallRot, wallContainer);
                    yield return new WaitForSeconds(stepSpeed);

                    cell.eastWall = GameObject.Instantiate(wall, positionVert, Quaternion.identity, wallContainer);
                    yield return new WaitForSeconds(stepSpeed);

                    if (j == ySize - 1)
                    {
                        Vector3 positionHorzBottom = new Vector3(GridContainer[i, j].x, GridContainer[i, j].y, GridContainer[i, j].z + wallBounds.extents.z);
                        cell.southWall = GameObject.Instantiate(wall, positionHorzBottom, wallRot, wallContainer);
                        yield return new WaitForSeconds(stepSpeed);
                    }

                    if (i == xSize - 1)
                    {
                        Vector3 positionVertSide = new Vector3(GridContainer[i, j].x + wallBounds.extents.z, GridContainer[i, j].y, GridContainer[i, j].z);
                        cell.westWall = GameObject.Instantiate(wall, positionVertSide, Quaternion.identity, wallContainer);
                        yield return new WaitForSeconds(stepSpeed);
                    }

                    cell.cellRow = j;
                    cell.cellColumn = i;
                    Cell.Maze.Add(cell);
                }
            }
            mazeCreated = true;
        }
    }

    public static void ClearGridLayout(Transform wallContainer)
    {
        if (mazeCreated)
        {
            foreach (Transform child in wallContainer)
            {
                GameObject.Destroy(child.gameObject);
            }
            Cell.Maze.Clear();
            mazeCreated = false;
        }
    }
}
