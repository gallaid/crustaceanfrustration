using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveMover : MonoBehaviour
{
    public GameObject stopPoint;
    public float Minutes;
    private float distance;
    private float speed;



    // Use this for initialization
    void Start()
    {
        distance = Vector3.Distance(stopPoint.transform.position,transform.position);
        speed = distance / (Minutes * 60);
        Mathf.Abs(speed);
    }

    // Update is called once per frame
    void Update()
    {



    }

    void FixedUpdate()
    {
        move();
    }

    private void move()
    {
       transform.position= Vector3.MoveTowards(transform.position, stopPoint.transform.position, speed*Time.deltaTime);
        //gameObject.transform.Translate(Vector3. * speed * Time.deltaTime);
        //transform.position=Vector3.Lerp(stopPoint.transform.position, gameObject.transform.position,1);
    }
}
