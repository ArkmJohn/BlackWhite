﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Marching Squares Method
/// </summary>

public class MeshGenerator : MonoBehaviour
{

    public SquareGrid squareGrid;
    public MeshFilter walls;
    public MeshFilter cave;
    public GameObject ground;
    public float wallHeight;
    public bool is2D;

    List<Vector3> vertices;
    List<int> triangles;

    Dictionary<int, List<Triangle>> triangleDict = new Dictionary<int, List<Triangle>>();
    List<List<int>> outlines = new List<List<int>>();
    HashSet<int> checkedVertices = new HashSet<int>();

    public void GenerateMesh(int[,] map, float squareSize)
    {
        //Resets variables
        triangleDict.Clear();
        outlines.Clear();
        checkedVertices.Clear();

        squareGrid = new SquareGrid(map, squareSize);

        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
        {
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
            {
                TriangSquare(squareGrid.squares[x, y]);
            }
        }

        Mesh mesh = new Mesh();
        cave.mesh = mesh;

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        int tileAmount = 10;
        Vector2[] uvs = new Vector2[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            float percentX = Mathf.InverseLerp(-map.GetLength(0) * squareSize, -map.GetLength(0) * squareSize, vertices[i].x) * tileAmount;
            float percentY = Mathf.InverseLerp(-map.GetLength(0) * squareSize, -map.GetLength(0) * squareSize, vertices[i].z) * tileAmount;

            uvs[i] = new Vector2(percentX, percentY);
        }
        mesh.uv = uvs;

        if (is2D)
        {
            Generete2DColliders();
        }
        else
        {
            CreateWallMesh();
        }
    }

    void Generete2DColliders()
    {

        EdgeCollider2D[] currentColliders = gameObject.GetComponents<EdgeCollider2D>();
        for (int i = 0; i < currentColliders.Length; i++)
        {
            Destroy(currentColliders[i]);
        }

        CalculateMeshOutlines();
        foreach (List<int> outline in outlines)
        {
            EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
            Vector2[] edgePoints = new Vector2[outline.Count];

            for (int i = 0; i < outline.Count; i++)
            {
                edgePoints[i] = new Vector2(vertices[outline[i]].x, vertices[outline[i]].z);
            }
            edgeCollider.points = edgePoints;
        }
    }

    void CreateWallMesh()
    {
        CalculateMeshOutlines();
        List<Vector3> wallVertices = new List<Vector3>();
        List<int> wallTriangles = new List<int>();
        Mesh wallMesh = new Mesh();

        foreach (List<int> outline in outlines)
        {
            for (int i = 0; i < outline.Count - 1; i++)
            {
                int startIndex = wallVertices.Count;
                wallVertices.Add(vertices[outline[i]]); // left 
                wallVertices.Add(vertices[outline[i + 1]]); // right 
                wallVertices.Add(vertices[outline[i]] - Vector3.up * wallHeight); // bottom left
                wallVertices.Add(vertices[outline[i + 1]] - Vector3.up * wallHeight); // bottom right  

                wallTriangles.Add(startIndex + 0);
                wallTriangles.Add(startIndex + 2);
                wallTriangles.Add(startIndex + 3);

                wallTriangles.Add(startIndex + 3);
                wallTriangles.Add(startIndex + 1);
                wallTriangles.Add(startIndex + 0);
            }

            wallMesh.vertices = wallVertices.ToArray();
            wallMesh.triangles = wallTriangles.ToArray();
            walls.mesh = wallMesh;
        }

        if (walls.gameObject.GetComponent<MeshCollider>() != null) // Destroys the previous colliders of the walls
        {
            ground.transform.position = Vector3.zero;
            Destroy(walls.gameObject.GetComponent<MeshCollider>());
        }
        walls.mesh.RecalculateNormals();
        ground.transform.position = new Vector3(gameObject.transform.position.x, -wallHeight, gameObject.transform.position.z);
        MeshCollider wallCollider = walls.gameObject.AddComponent<MeshCollider>();
        wallCollider.sharedMesh = wallMesh;
        wallCollider.material.dynamicFriction = 1;
        wallCollider.material.staticFriction = 1;
        wallCollider.material.bounciness = 1;
    }

    void TriangSquare(Square square) // Assigns Nodes from the square to a triangle to calulate
    {
        switch (square.config)
        {
            case 0:
                break;

            // 1 Point
            case 1:
                MeshFromPoints(square.centreLeft, square.centreBottom, square.bottomLeft);
                break;
            case 2:
                MeshFromPoints(square.bottomRight, square.centreBottom, square.centreRight);
                break;
            case 4:
                MeshFromPoints(square.topRight, square.centreRight, square.centreTop);
                break;
            case 8:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreLeft);
                break;

            // 2 Points
            case 3:
                MeshFromPoints(square.centreRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 6:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.centreBottom);
                break;
            case 9:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreBottom, square.bottomLeft);
                break;
            case 12:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreLeft);
                break;
            case 5:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft, square.centreLeft);
                break;
            case 10:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 3 Points
            case 7:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 11:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.bottomLeft);
                break;
            case 13:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft);
                break;
            case 14:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 4 Points
            case 15:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
                checkedVertices.Add(square.topLeft.vertexIndex);
                checkedVertices.Add(square.topRight.vertexIndex);
                checkedVertices.Add(square.bottomRight.vertexIndex);
                checkedVertices.Add(square.bottomLeft.vertexIndex);

                break;
        }
    }

    struct Triangle // For the points of the mesh
    {
        public int vertexIndexA;
        public int vertexIndexB;
        public int vertexIndexC;
        int[] vertices;

        public Triangle(int a, int b, int c)
        {
            vertexIndexA = a;
            vertexIndexB = b;
            vertexIndexC = c;

            vertices = new int[3];
            vertices[0] = a;
            vertices[1] = b;
            vertices[2] = c;
        }

        public int this[int i]
        {
            get
            {
                return vertices[i];
            }
        }

        public bool Contains(int vertexIndex)
        {
            return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;
        }
    }

    void MeshFromPoints(params Node[] points)
    {
        AssignVertices(points);

        if (points.Length >= 3)
            CreateTriangle(points[0], points[1], points[2]);
        if (points.Length >= 4)
            CreateTriangle(points[0], points[2], points[3]);
        if (points.Length >= 5)
            CreateTriangle(points[0], points[3], points[4]);
        if (points.Length >= 6)
            CreateTriangle(points[0], points[4], points[5]);
    }

    void AssignVertices(Node[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].vertexIndex == -1)
            {
                points[i].vertexIndex = vertices.Count;
                vertices.Add(points[i].position);

            }
        }
    }

    void CreateTriangle(Node a, Node b, Node c)
    {
        triangles.Add(a.vertexIndex);
        triangles.Add(b.vertexIndex);
        triangles.Add(c.vertexIndex);

        Triangle triangle = new Triangle(a.vertexIndex, b.vertexIndex, c.vertexIndex);
        AddTriangleToDictionary(triangle.vertexIndexA, triangle);
        AddTriangleToDictionary(triangle.vertexIndexB, triangle);
        AddTriangleToDictionary(triangle.vertexIndexC, triangle);
    }

    void AddTriangleToDictionary(int vertexIndexKey, Triangle triangle)
    {
        if (triangleDict.ContainsKey(vertexIndexKey))
        {
            triangleDict[vertexIndexKey].Add(triangle);

        }
        else
        {
            List<Triangle> triangleList = new List<Triangle>();
            triangleList.Add(triangle);
            triangleDict.Add(vertexIndexKey, triangleList);
        }
    }

    void CalculateMeshOutlines()
    {
        for (int vertexIndex = 0; vertexIndex < vertices.Count; vertexIndex++)
        {
            if (!checkedVertices.Contains(vertexIndex))
            {
                int newOutlineVertex = GetConnectedOutlineVertex(vertexIndex);

                if (newOutlineVertex != -1)
                {
                    checkedVertices.Add(vertexIndex);

                    List<int> newOutline = new List<int>();
                    newOutline.Add(vertexIndex);

                    outlines.Add(newOutline);

                    FollowOutline(newOutlineVertex, outlines.Count - 1);

                    outlines[outlines.Count - 1].Add(vertexIndex);
                }
            }
        }
    }

    void FollowOutline(int vertexIndex, int outlineIndex)
    {
        outlines[outlineIndex].Add(vertexIndex);
        checkedVertices.Add(vertexIndex);
        int nextVertexIndex = GetConnectedOutlineVertex(vertexIndex);

        if (nextVertexIndex != -1)
        {
            FollowOutline(nextVertexIndex, outlineIndex);
        }
    }

    int GetConnectedOutlineVertex(int vertexIndex)
    {
        List<Triangle> triangleContainingVertex = triangleDict[vertexIndex];

        for (int i = 0; i < triangleContainingVertex.Count; i++)
        {
            Triangle triangle = triangleContainingVertex[i];

            for (int j = 0; j < 3; j++)
            {
                int vertexB = triangle[j];
                if (vertexB != vertexIndex && !checkedVertices.Contains(vertexB))
                {
                    if (IsOutlineEdge(vertexIndex, vertexB))
                    {
                        return vertexB;
                    }
                }
            }
        }

        return -1;
    }

    bool IsOutlineEdge(int vertexA, int vertexB)
    {
        List<Triangle> trianglesContainingVertexA = triangleDict[vertexA];
        int sharedTriangleCount = 0;

        for (int i = 0; i < trianglesContainingVertexA.Count; i++)
        {
            if (trianglesContainingVertexA[i].Contains(vertexB))
            {
                sharedTriangleCount++;
                if (sharedTriangleCount > 1)
                {
                    break;
                }
            }
        }

        return sharedTriangleCount == 1;
    }

    public class SquareGrid
    {
        public Square[,] squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);
            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            CNode[,] controlNodes = new CNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, 0, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new CNode(pos, map[x, y] == 1, squareSize);
                }

            }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];
            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x, y], controlNodes[x + 1, y]);
                }

            }
        }
    }

    public class Square
    {
        public CNode topLeft, topRight, bottomLeft, bottomRight;
        public Node centreTop, centreRight, centreBottom, centreLeft;
        public int config;

        public Square(CNode _topLeft, CNode _topRight, CNode _bottomLeft, CNode _bottomRight)
        {
            topLeft = _topLeft;
            topRight = _topRight;
            bottomLeft = _bottomLeft;
            bottomRight = _bottomRight;

            centreTop = topLeft.right;
            centreRight = bottomRight.up;
            centreBottom = bottomLeft.right;
            centreLeft = bottomLeft.up;

            if (topLeft.active)
                config += 8;
            if (topRight.active)
                config += 4;
            if (bottomRight.active)
                config += 2;
            if (bottomLeft.active)
                config += 1;
        }
    }

    /// <summary>
    /// Node Summary
    /// CN - N - CN
    /// |    |    |
    /// N  - -  - N
    /// |    |    |
    /// CN - N - CN
    /// 
    /// One Central Node is = to a edge in a square
    /// </summary>

    public class Node
    {
        public Vector3 position;
        public int vertexIndex = -1;

        public Node(Vector3 _pos)
        {
            position = _pos;
        }
    }

    public class CNode : Node // Control Nodes
    {
        public bool active; // wall or not
        public Node up, right;

        public CNode(Vector3 _pos, bool _active, float squareSize) : base(_pos)
        {
            active = _active;
            up = new Node(position + Vector3.forward * squareSize / 2f);
            right = new Node(position + Vector3.right * squareSize / 2f);
        }
    }
}
