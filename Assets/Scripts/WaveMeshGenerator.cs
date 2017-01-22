using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveMeshGenerator : MonoBehaviour {

    [Header("Mesh Size")]
    public int RowVertices = 50;
    public int ColumnVertices = 50;
    public float scale = 1.0f;

    /*
    [Header("Waves")]
    public float amplitude = 1.0f;
    public float speed = 1.0f;
    public float frequency = 1.0f;
    */

    private Mesh mesh;

    private Vector3[] baseVertices;
    private Vector3[] deformedVertices;
    private int n;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;

        GeneratePlane(RowVertices, ColumnVertices);
    }

    void GeneratePlane(int rows, int cols)
    {
        mesh.Clear();
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

                newVertices[i++] = vpos * tri_height;
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

                if((col & 1) == 0)
                {
                    // Bottom face
                    newTriangles[i++] = baseVertex;
                    newTriangles[i++] = baseVertex + 1;
                    newTriangles[i++] = nextRowVertex;
                    

                    // Top face
                    newTriangles[i++] = nextRowVertex;
                    newTriangles[i++] = baseVertex + 1;
                    newTriangles[i++] = nextRowVertex + 1;
                    
                } else
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
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        baseVertices = (Vector3[]) newVertices.Clone();
        deformedVertices = (Vector3[])newVertices.Clone();
    }

    /*
    void DeformMesh()
    {
        float t = Time.time;
        float a = amplitude;

        for(int i = 0; i < n; i++)
        {
            Vector3 baseVertex = baseVertices[i];
            float phase = baseVertex.x * frequency + t*speed;
            Vector3 displacement = new Vector3(a * Mathf.Cos(phase), a * Mathf.Sin(phase), 0);
            deformedVertices[i] = baseVertex + displacement;
        }

        mesh.vertices = deformedVertices;
        mesh.RecalculateNormals();
    }
    */

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //DeformMesh();

    }
}
