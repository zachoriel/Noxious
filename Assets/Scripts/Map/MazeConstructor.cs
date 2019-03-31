using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeConstructor : MonoBehaviour
{
    public static MazeConstructor instance;

    public bool showDebug;

    [Header("Materials")]
    [SerializeField] Material mazeMat1;
    [SerializeField] Material mazeMat2;

    public int[,] data
    {
        get; private set;
    }

    public float hallWidth
    {
        get; private set;
    }
    public float hallHeight
    {
        get; private set;
    }

    public int startRow
    {
        get; private set;
    }
    public int startCol
    {
        get; private set;
    }

    public int goalRow
    {
        get; private set;
    }
    public int goalCol
    {
        get; private set;
    }

    MazeDataGenerator dataGenerator;
    MazeMeshGenerator meshGenerator;

	void Awake ()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion

        dataGenerator = new MazeDataGenerator();
        meshGenerator = new MazeMeshGenerator();

        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
	}

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Please use odd numbers.");
        }

        DisposeOldMaze();

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);

        hallWidth = meshGenerator.width;
        hallHeight = meshGenerator.height;

        DisplayMaze();
    }

    void DisplayMaze()
    {
        GameObject newMaze = new GameObject();
        newMaze.isStatic = true;
        newMaze.transform.position = Vector3.zero;
        newMaze.name = "Procedural Maze";
        newMaze.tag = "Generated";
        newMaze.layer = 8;

        MeshFilter meshFilter = newMaze.AddComponent<MeshFilter>();
        meshFilter.mesh = meshGenerator.FromData(data);

        MeshCollider meshCollider = newMaze.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;

        MeshRenderer meshRenderer = newMaze.AddComponent<MeshRenderer>();
        meshRenderer.materials = new Material[2] { mazeMat1, mazeMat2 };

        NavMeshSurface surface = newMaze.AddComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject _object in objects)
        {
            Destroy(_object);
        }
    }

    void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        int[,] maze = data;
        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);

        string message = "";

        for (int i = rowMax; i >= 0; i--)
        {
            for (int j = 0; j <= colMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    message += "....";
                }
                else if (maze[i, j] == 1)
                {
                    message += "==";
                }
            }
            message += "\n";
        }

        GUI.Label(new Rect(20, 20, 500, 500), message);
    }
}
