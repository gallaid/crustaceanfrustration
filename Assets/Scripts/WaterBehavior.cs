using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class WaterBehavior : MonoBehaviour
{
    [Header("Mesh Size")]
    public int RowVertices = 50;
    public int ColumnVertices = 50;
    public float scale = 1.0f;

    [Header("Wave effect")]
    public GameObject water;
    Mesh deformingMesh;
    public float maxDistance;
    public float curl;
    public float falloff;
    Vector3[] originalVertices, displacedVertices;

    private int n;

    private void Awake()
    {
        deformingMesh = water.GetComponent<MeshFilter>().sharedMesh;
        GeneratePlane(RowVertices, ColumnVertices);

    }

    private void Start()
    {
        /*
        deformingMesh = water.GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices.Clone() as Vector3[];
        displacedVertices = new Vector3[originalVertices.Length];
        Vector3[] vertexVelocities;
        for (int i = 0; i < originalVertices.Length; i++)
        {
            //print(i + "this is i");
            displacedVertices[i] = originalVertices[i];
            vertexVelocities = new Vector3[originalVertices.Length];

        }
        */

    }

    public void RegeneratePlane()
    {
        deformingMesh = water.GetComponent<MeshFilter>().sharedMesh;
        GeneratePlane(RowVertices, ColumnVertices);
    }

    void GeneratePlane(int rows, int cols)
    {
        deformingMesh.Clear();
        n = rows * cols;
        Vector3[] newVertices = new Vector3[n];
        Vector2[] newUV = new Vector2[n];
        int[] newTriangles = new int[n * 6];

        int i = 0;
        Vector3 centerpoint = new Vector3(rows / 2.0f, 0, cols / 2.0f);

        // Offset to make a unit triangle. This is the height
        float tri_height = Mathf.Sqrt(3.0f) / 2.0f;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 vpos = new Vector3(row, 0, col);
                vpos -= centerpoint;

                // Every other row is shifted slightly for the simplex grid
                if ((i & 1) != 0)
                {
                    vpos += new Vector3(0.5f, 0, 0);
                }

                newVertices[i++] = vpos * tri_height * scale;
            }
        }

        // Link the vertices into a mesh
        i = 0;
        for (int row = 0; row < rows - 1; row++)
        {
            for (int col = 0; col < cols - 1; col++)
            {
                int baseVertex = col + (row * cols);
                int nextRowVertex = col + ((row + 1) * cols);

                if ((col & 1) == 0)
                {
                    // Bottom face
                    newTriangles[i++] = baseVertex;
                    newTriangles[i++] = baseVertex + 1;
                    newTriangles[i++] = nextRowVertex;


                    // Top face
                    newTriangles[i++] = nextRowVertex;
                    newTriangles[i++] = baseVertex + 1;
                    newTriangles[i++] = nextRowVertex + 1;

                }
                else
                {
                    // Bottom face
                    newTriangles[i++] = baseVertex;
                    newTriangles[i++] = baseVertex + 1;
                    newTriangles[i++] = nextRowVertex + 1;


                    // Top face
                    newTriangles[i++] = baseVertex;
                    newTriangles[i++] = nextRowVertex + 1;
                    newTriangles[i++] = nextRowVertex;

                }

            }
        }

        // Assign back to the mesh
        deformingMesh.vertices = newVertices;
        deformingMesh.triangles = newTriangles;
        deformingMesh.RecalculateNormals();

        originalVertices = (Vector3[])newVertices.Clone();
        displacedVertices = (Vector3[])newVertices.Clone();
    }


    private void Update()
    {

        //print(originalVertices + " oV");
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = MoveVertices(originalVertices[i]);
            

        }
        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();

    }


    Vector3 MoveVertices(Vector3 Vertex)
    {
        Vector3 attractorSpace;
        attractorSpace=transform.InverseTransformPoint(water.transform.TransformPoint(Vertex));
        float distance = Vector3.Distance(attractorSpace, Vector3.zero);
        float distanceSq = Vector3.SqrMagnitude(attractorSpace);
        if (distanceSq <= maxDistance*maxDistance)
        {
            float baseAngle = (curl) * Mathf.Pow(distanceSq, -falloff);
            
            Quaternion rot = Quaternion.AngleAxis(baseAngle, Vector3.back);
            attractorSpace = rot * attractorSpace;
            Vertex = water.transform.InverseTransformPoint(transform.TransformPoint(attractorSpace));
            return (Vertex);
        }

        return Vertex;
    }

}