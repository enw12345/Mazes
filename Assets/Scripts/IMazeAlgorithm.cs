﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IMazeAlgorithm
{
    void RemoveWall(GameObject wall);

    void GenerateMaze();

    IEnumerator GenerateMazeStep(float stepSpeed);
}

