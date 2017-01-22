using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
public class WaterBehavior : MonoBehaviour
{
    public GameObject water;
    Mesh deformingMesh;
    public float maxDistance;
    public float curl;
    Vector3[] originalVertices, displacedVertices;

    private void Start()
    {
        
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

    }




    private void Update()
    {
       
        //print(originalVertices + " oV");
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = MoveVertices(originalVertices[i]);
            

        }
        deformingMesh.vertices = displacedVertices;


    }


    Vector3 MoveVertices(Vector3 Vertex)
    {
        Vector3 attractorSpace;
        attractorSpace=transform.InverseTransformPoint(water.transform.TransformPoint(Vertex));
        float distance = Vector3.Distance(attractorSpace, Vector3.zero);

        if (distance <= maxDistance)
        {
            float baseAngle = (curl)/distance;
            
            Quaternion rot = Quaternion.AngleAxis(baseAngle, Vector3.back);
            attractorSpace = rot * attractorSpace;
            Vertex = water.transform.InverseTransformPoint(transform.TransformPoint(attractorSpace));
            return (Vertex);
        }

        return Vertex;
    }

}