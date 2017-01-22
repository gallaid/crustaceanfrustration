using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaterBehavior))]
public class WaveAttractorEditor : Editor {

	// Use this for initialization
	public override void OnInspectorGUI()
    {
        WaterBehavior attractor = (WaterBehavior)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate Mesh"))
        {
            attractor.RegeneratePlane();
        }
    }
}
