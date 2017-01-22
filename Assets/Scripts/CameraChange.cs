using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {
    public GameObject CameraShot;
    public GameObject Camera;
    public GameObject TargetSpot;
    bool MoveCamera = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z>TargetSpot.transform.position.z)
        {
            if (!MoveCamera)
            {
                MonoBehaviour[] comps = Camera.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour c in comps)
                {
                    c.enabled = false;
                }
                MoveCamera = true;
            }

            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CameraShot.transform.position, 3);
            Camera.transform.LookAt(TargetSpot.transform.position);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        print("heretrigger");
        if (other.tag == "WorldEnder")
        {
            print("here");
            MonoBehaviour[] comps = Camera.GetComponents<MonoBehaviour>();
            foreach(MonoBehaviour c in comps)
            {
                c.enabled = false;
                
            }
            MoveCamera = true;
            
        }
    }
}
