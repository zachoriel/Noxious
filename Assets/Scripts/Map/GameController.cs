﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    public MazeConstructor generator;

	void Start ()
    {
        if (generator == null)
        {
            generator = GetComponent<MazeConstructor>();
        }

        generator.GenerateNewMaze(13, 15);
	}
}