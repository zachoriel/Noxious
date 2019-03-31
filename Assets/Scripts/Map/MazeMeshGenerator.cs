using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMeshGenerator : MonoBehaviour
{
    // Generator parameters
    public float width; // How wide the hallways are
    public float height; // How tall the hallways are

    public MazeMeshGenerator()
    {
        width = 20f;
        height = 20f;
    }

    public Mesh FromData(int[,] data)
    {
        Mesh maze = new Mesh();

        #region Mesh Lists
        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();

        maze.subMeshCount = 2;
        List<int> floorTriangles = new List<int>();
        List<int> wallTriangles = new List<int>();
        #endregion

        int rowMax = data.GetUpperBound(0);
        int colMax = data.GetUpperBound(1);
        float halfHeight = height * 0.5f;

        for (int i = 0; i <= rowMax; i++)
        {
            for (int j = 0; j<= colMax; j++)
            {
                if (data[i, j] != 1)
                {
                    // Floor
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, 0, i * width),
                        Quaternion.LookRotation(Vector3.up),
                        new Vector3(width, width, 1)
                    ), ref newVertices, ref newUVs, ref floorTriangles);

                    // Ceiling
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, height, i * width),
                        Quaternion.LookRotation(Vector3.down),
                        new Vector3(width, width, 1)
                    ), ref newVertices, ref newUVs, ref floorTriangles);

                    // Walls on sides next to blocked grid cells

                    if (i - 1 < 0 || data[i - 1, j] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfHeight, (i - .5f) * width),
                            Quaternion.LookRotation(Vector3.forward),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (j + 1 > colMax || data[i, j + 1] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j + .5f) * width, halfHeight, i * width),
                            Quaternion.LookRotation(Vector3.left),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (j - 1 < 0 || data[i, j - 1] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j - .5f) * width, halfHeight, i * width),
                            Quaternion.LookRotation(Vector3.right),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (i + 1 > rowMax || data[i + 1, j] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfHeight, (i + .5f) * width),
                            Quaternion.LookRotation(Vector3.back),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }
                }
            }
        }

        maze.vertices = newVertices.ToArray();
        maze.uv = newUVs.ToArray();

        maze.SetTriangles(floorTriangles.ToArray(), 0);
        maze.SetTriangles(wallTriangles.ToArray(), 1);

        maze.RecalculateNormals();

        return maze;
    }

    void AddQuad(Matrix4x4 matrix, ref List<Vector3> newVertices,
        ref List<Vector2> newUVs, ref List<int> newTriangles)
    {
        int index = newVertices.Count;

        // Corners before transforming
        Vector3 vert1 = new Vector3(-0.5f, -0.5f, 0);
        Vector3 vert2 = new Vector3(-0.5f, 0.5f, 0);
        Vector3 vert3 = new Vector3(0.5f, 0.5f, 0);
        Vector3 vert4 = new Vector3(0.5f, -0.5f, 0);

        // Add vertices to list
        newVertices.AddMany(matrix.MultiplyPoint3x4(vert1),
            matrix.MultiplyPoint3x4(vert2),
            matrix.MultiplyPoint3x4(vert3),
            matrix.MultiplyPoint3x4(vert4));

        // Add uvs to list
        newUVs.AddMany(new Vector2(1, 0), new Vector2(1, 1), 
            new Vector2(0, 1), new Vector2(0, 0));

        // Add triangles to list
        newTriangles.AddMany(index + 2, index + 1, index);
        newTriangles.AddMany(index + 3, index + 2, index);
    }
}
