using System.Collections;
using UnityEngine;

/// <summary>
/// 1. Can start anywhere
/// 2. Must visit every cell
/// 3. Must remove a wall at every cell (either north or east)
/// </summary>
public class BinaryMazeAlgorithm : IMazeAlgorithm
{
    public void RemoveWall(GameObject wall)
    {
        wall.SetActive(false);
    }

    public void GenerateMaze()
    {
        for (int i = 0; i < Cell.Maze.Count - 1; i++)
        {
            Cell currentCell = Cell.Maze[i + 1];

            if (!currentCell.visited)
            {
                if (currentCell.CellPos.x == 0)
                    RemoveWall(currentCell.eastWall);

                else if (currentCell.CellPos.y == 0)
                    RemoveWall(currentCell.northWall);

                else
                {
                    int toRemove = Random.Range(1, 101);

                    if (toRemove < 50)
                        RemoveWall(currentCell.northWall);

                    else
                        RemoveWall(currentCell.eastWall);
                }

                currentCell.visited = true;
            }
        }
    }

    public IEnumerator GenerateMazeStep(float stepSpeed)
    {
        for (int i = 0; i < Cell.Maze.Count - 1; i++)
        {
            Cell currentCell = Cell.Maze[i + 1];

            if (!currentCell.visited)
            {
                if (currentCell.CellPos.x == 0)
                {
                    currentCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(currentCell.eastWall);
                }

                else if (currentCell.CellPos.y == 0)
                {
                    currentCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(currentCell.northWall);
                }

                else
                {
                    int toRemove = Random.Range(1, 101);

                    if (toRemove < 50)
                    {
                        currentCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                        yield return new WaitForSeconds(stepSpeed);
                        RemoveWall(currentCell.northWall);
                    }

                    else
                    {
                        currentCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                        yield return new WaitForSeconds(stepSpeed);
                        RemoveWall(currentCell.eastWall);
                    }
                }

                currentCell.visited = true;
            }
        }
    }
}

