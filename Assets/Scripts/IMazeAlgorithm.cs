using System.Collections;
using UnityEngine;

public interface IMazeAlgorithm
{
    void RemoveWall(GameObject wall);

    void GenerateMaze();

    IEnumerator GenerateMazeStep(float stepSpeed);
}

