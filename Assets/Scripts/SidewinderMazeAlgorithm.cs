using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Start from the western most column
/// 2. Must visit every cell
/// 3. Has "runs" where it remebers every cell its visited for each run
/// 4. When a run ends it must remove a north wall from a random cell in the run
/// </summary>

public class SidewinderMazeAlgorithm : IMazeAlgorithm
{
    private List<Cell> Run = new List<Cell>();

    public void GenerateMaze()
    {
        int start = (Cell.Maze.Count) - Grid.cellCountX;

        //start at the first cell in the western most column
        for (int i = 0; i < Grid.cellCountY; i++)
        {
            //go through the grid row by row
            for (int j = start; j > -4; j -= Grid.cellCountX)
            {
                Cell currentCell = Cell.Maze[j + i];

                // go through and remove each wall
                int toRemove = Random.Range(0, 101);
                if (currentCell.visited == false)
                {
                    //If we are at the end of a row we close the run or we randomly close the run.
                    if (currentCell.CellPos.y == 0 && currentCell.CellPos.x != 0)
                    {
                        RemoveWall(currentCell.eastWall);
                    }
                    else
                    {
                        if (currentCell.CellPos.x == 0 && currentCell.CellPos.y != 0 || currentCell.CellPos.y != 0 && toRemove < 50)
                        {
                            Cell randomCell;

                            if (Run.Count > 0)
                                randomCell = Run[Random.Range(0, Run.Count)];
                            else
                                randomCell = currentCell;

                            RemoveWall(randomCell.northWall);

                            //End the run
                            Run.Clear();
                        }

                        else
                        {
                            if (currentCell.CellPos.y != 0)
                            {
                                RemoveWall(currentCell.eastWall);
                                Run.Add(currentCell);
                            }
                        }
                    }

                    currentCell.visited = true;
                }
            }
        }
    }

    public IEnumerator GenerateMazeStep(float stepSpeed)
    {
        int start = (Cell.Maze.Count) - Grid.cellCountX;

        //start at the first cell in the western most column
        for (int i = 0; i < Grid.cellCountY; i++)
        {
            //go through the grid row by row
            for (int j = start; j > -4; j -= Grid.cellCountX)
            {
                Cell currentCell = Cell.Maze[j + i];

                // go through and remove each wall
                int toRemove = Random.Range(0, 101);
                if (currentCell.visited == false)
                {
                    //If we are at the end of a row we close the run or we randomly close the run.
                    if (currentCell.CellPos.y == 0 && currentCell.CellPos.x != 0)
                    {
                        currentCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                        yield return new WaitForSeconds(stepSpeed);
                        RemoveWall(currentCell.eastWall);
                    }
                    else
                    {
                        if (currentCell.CellPos.x == 0 && currentCell.CellPos.y != 0 || currentCell.CellPos.y != 0 && toRemove < 50)
                        {
                            Cell randomCell;

                            if (Run.Count > 0)
                                randomCell = Run[Random.Range(0, Run.Count)];
                            else
                                randomCell = currentCell;

                            randomCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                            yield return new WaitForSeconds(stepSpeed);
                            RemoveWall(randomCell.northWall);

                            //End the run
                            Run.Clear();
                        }

                        else
                        {
                            if(currentCell.CellPos.y != 0)
                            {
                                currentCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                                yield return new WaitForSeconds(stepSpeed);
                                RemoveWall(currentCell.eastWall);
                                Run.Add(currentCell);
                            }
                        }
                    }

                    currentCell.visited = true;
                }
            }
        }
    }

    public void RemoveWall(GameObject wall)
    {
        wall.SetActive(false);
    }
}
