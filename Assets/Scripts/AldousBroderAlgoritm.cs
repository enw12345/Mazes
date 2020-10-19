using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An un biased, uniform, random walk maze
/// 1. Start  anywhere in the grid
/// 2. Choose a random nneighbor
/// 3. Move to the neighbor if not previiously visited
/// 4. Link it to the prior cell
/// 5. Repeat until every cell has been visited
/// </summary>

class AldousBroderAlgorithm : IMazeAlgorithm
{
    public void GenerateMaze()
    {

    }

    public IEnumerator GenerateMazeStep(float stepSpeed)
    {
        int unvisitedCells = Cell.Maze.Count - 1;
        Debug.Log("Unvisted Cells Start: " + unvisitedCells);
        Cell currentCell;

        //select a random cell
        Cell startCell = Cell.Maze[Random.Range(0, Cell.Maze.Count)];
        currentCell = startCell;

        //The algorithm runs until all of the cells have been visited
        while (unvisitedCells != 0)
        {
            List<int> neighborCellsIndex = new List<int>();

            if (!currentCell.visited)
            {
                currentCell.visited = true;
                unvisitedCells--;

                int southNeighborIndex = currentCell.CellIndex + 1;
                int northNeighborIndex = currentCell.CellIndex - 1;
                int westNeighborIndex = currentCell.CellIndex + Grid.cellCountY;
                int eastNeighborIndex = currentCell.CellIndex - Grid.cellCountY;

                if (currentCell.CellPos.y < Grid.cellCountY - 1)
                {
                    neighborCellsIndex.Add(southNeighborIndex);
                }
                else if (currentCell.CellPos.y > 0)
                {
                    neighborCellsIndex.Add(northNeighborIndex);
                }
                else if (currentCell.CellPos.x < Grid.cellCountX - 1)
                {
                    neighborCellsIndex.Add(westNeighborIndex);
                }
                else if (currentCell.CellPos.x > 0)
                {
                    neighborCellsIndex.Add(eastNeighborIndex);
                }

                Debug.Log(currentCell.CellIndex + " cell has " + neighborCellsIndex.Count + " neighbor cells.");

                int index = neighborCellsIndex[Random.Range(0, neighborCellsIndex.Count)];
                neighborCellsIndex.Clear();
                Debug.Log(index);

                Cell neighborCell = Cell.Maze[index];

                Debug.Log("Current cell: " + currentCell.CellIndex + " Neighbor Cell: " + neighborCell.CellIndex);

                if (neighborCell.CellIndex == southNeighborIndex)
                {
                    neighborCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(neighborCell.northWall);
                }
                else if (neighborCell.CellIndex == northNeighborIndex)
                {
                    currentCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(currentCell.northWall);
                }
                else if (neighborCell.CellIndex == westNeighborIndex)
                {
                    neighborCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(neighborCell.eastWall);
                }
                else if (neighborCell.CellIndex == eastNeighborIndex)
                {
                    currentCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(currentCell.eastWall);
                }

                currentCell = neighborCell;
            }

            else
            {
                currentCell = Cell.Maze[Random.Range(0, Cell.Maze.Count)];
            }
        }
    }

    public void RemoveWall(GameObject wall)
    {
        wall.SetActive(false);
    }

    private bool AreAllCellsVisited()
    {
        int unvisitedCells = 0;

        for (int i = 0; i < Cell.Maze.Count - 1; i++)
        {
            Cell currentCell = Cell.Maze[i];
            if (currentCell.visited)
                unvisitedCells--;
            else
                unvisitedCells++;
        }

        if (unvisitedCells == 0)
            return true;
        else
            return false;
    }
}

