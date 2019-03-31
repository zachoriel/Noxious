using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    public float placementThreshold; // Chance of empty space

    public MazeDataGenerator()
    {
        placementThreshold = 0.1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];

        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rowMax; i++)
        {
            for (int j = 0; j <= colMax; j++)
            {
                if (i == 0 || j == 0 || i == rowMax || j == colMax)
                {
                    maze[i, j] = 1;
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > placementThreshold)
                    {
                        maze[i, j] = 1;

                        int a = Random.value < 0.5f ? 0 : (Random.value < 0.5f ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < 0.5f ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }

        return maze;
    }
}
