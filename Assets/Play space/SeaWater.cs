using UnityEngine;
using System.Collections;

public class SeaWater : MonoBehaviour {

    public float UpperBound = -3.4f;
    public float LowerBound = -8.4f;
    private bool dirRight = true;
    public float speed = 2.0f;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.up * speed * Time.deltaTime);

        if (transform.position.y >= UpperBound)
        {
            dirRight = false;
        }

        if (transform.position.y <= LowerBound)
        {
            dirRight = true;
        }
    }
}
