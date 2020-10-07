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
        throw new System.NotImplementedException();
    }

    public IEnumerator GenerateMazeStep(float stepSpeed)
    {
        //start at a random cell in the western most column
        //Cell startCell = Cell.Maze[Random.Range(Cell.Maze.Count - MazeBase.cellCountX, Cell.Maze.Count - 1)];

        //start at the first cell in the western most column
        for (int i = 0; i < Grid.cellCountY; i++)
        {
            //go through the grid row by row
            for (int j = ((Cell.Maze.Count - 1) - Grid.cellCountX); j >= 0; j -= Grid.cellCountX)
            {
                int index = (Cell.Maze.Count - 1) - j + i;
                Cell currentCell = Cell.Maze[index];

                if (currentCell.CellIndex == Vector2.zero)
                {
                    yield return new WaitForSeconds(stepSpeed);
                }
                else if (currentCell.CellIndex.x == 0)
                {
                    currentCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(currentCell.northWall);
                }
                else if (currentCell.CellIndex.y == 0)
                {
                    currentCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                    yield return new WaitForSeconds(stepSpeed);
                    RemoveWall(currentCell.eastWall);
                }
                else
                {
                    // go through and remove each wall
                    int toRemove = Random.Range(0, 101);
                    if (toRemove < 50)
                    {
                        Debug.Log("Carving east");
                        //add cell to the run and remove the eastwall
                        currentCell.eastWall.GetComponent<MeshRenderer>().material.color = Color.red;
                        Run.Add(currentCell);
                        yield return new WaitForSeconds(stepSpeed);
                        RemoveWall(currentCell.eastWall);
                    }
                    else
                    {
                        //remove a north wall from a random cell in the run
                        if (Run.Count > 0)
                        {
                            Debug.Log("Carving Random north");
                            Cell randomCell = Run[Random.Range(0, Run.Count)];
                            randomCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                            yield return new WaitForSeconds(stepSpeed);
                            RemoveWall(randomCell.northWall);

                            //End the run
                            Run.Clear();
                        }
                        //else
                        //{
                        //    Debug.Log("Carving north");
                        //    currentCell.northWall.GetComponent<MeshRenderer>().material.color = Color.red;
                        //    Run.Add(currentCell);
                        //    yield return new WaitForSeconds(stepSpeed);
                        //    RemoveWall(currentCell.northWall);
                        //}

                    }
                }

                currentCell.visited = true;
            }
        }

        yield return new WaitForSeconds(stepSpeed);
    }

    public void RemoveWall(GameObject wall)
    {
        wall.SetActive(false);
    }
}
