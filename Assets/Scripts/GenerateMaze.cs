﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    public int sizeX = 4;
    public int sizeY = 4;
    public float top = 0;

    public Transform maze;
    public GameObject wall;
    private Bounds wallBounds;

    public enum MazeTypes { Binary};
    // Start is called before the first frame update
    void Awake()
    {
        wallBounds = wall.GetComponent<MeshFilter>().sharedMesh.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Maze.CreateGridLayout(maze, wall, wallBounds, sizeX, sizeY, top);
        }
    }
}
