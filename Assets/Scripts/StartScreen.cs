using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

   public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.transform.position.y < -2)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetAxis("Quit") > 0)
        {
            print("Quit");
            Application.Quit();
        }
	}

    private void OnTriggerEnter(Collider other)
    {

    }
}
