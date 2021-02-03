using System.Collections;
using System.Collections.Generic;
using System.Data;
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

        //select a random cell
        Cell currentCell = Cell.Maze[Random.Range(0, Cell.Maze.Count)];

        //The algorithm runs until all of the cells have been visited
        while (unvisitedCells != 0)
        {
            List<int> neighborCells = new List<int>();

            if (!currentCell.visited)
            {
                currentCell.visited = true;
                unvisitedCells--;

                int southernNeighbor = currentCell.CellIndex + 1;
                int northernNeighbor = currentCell.CellIndex - 1;
                int easternNeighbor = currentCell.CellIndex - Grid.cellCountY;
                int westernNeighbor = currentCell.CellIndex + Grid.cellCountY;
                int gridSize = Grid.cellCountX * Grid.cellCountY;

                if (currentCell.cellRow != Grid.cellCountY - 1)
                {
                    if (southernNeighbor > 0 && southernNeighbor < gridSize)
                        neighborCells.Add(southernNeighbor);
                }
                if (currentCell.cellRow != 0)
                {
                    if (northernNeighbor > 0 && northernNeighbor < gridSize)
                        neighborCells.Add(northernNeighbor);
                }
                if (currentCell.cellColumn != 0)
                {
                    if (easternNeighbor > 0 && easternNeighbor < gridSize)
                        neighborCells.Add(easternNeighbor);
                }
                if (currentCell.cellColumn != Grid.cellCountX - 1)
                {
                    if (westernNeighbor > 0 && westernNeighbor < gridSize)
                        neighborCells.Add(westernNeighbor);
                }

                int index = neighborCells[Random.Range(0, neighborCells.Count)];
                neighborCells.Clear();

                Cell neighborCell = Cell.Maze[index];

                if (neighborCell.CellIndex == southernNeighbor)
                {
                    neighborCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(neighborCell.northWall);
                }
                else if (neighborCell.CellIndex == northernNeighbor)
                {
                    currentCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(currentCell.northWall);
                }
                else if (neighborCell.CellIndex == westernNeighbor)
                {
                    neighborCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(neighborCell.eastWall);
                }
                else if (neighborCell.CellIndex == easternNeighbor)
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
}

