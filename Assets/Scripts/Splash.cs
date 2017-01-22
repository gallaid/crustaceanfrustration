using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

    private ParticleSystem particles;
    void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("Splash");
        if (other.tag == "WorldEnder")
        {
            particles.Play();
        }
    }
}
