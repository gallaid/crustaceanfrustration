using System;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float _jumpForce = 10;

    public float _velocity = 1;

    private enum MoveDirection
    {
        Forward,
        Back,
        Left,
        Right
    }

    private Dictionary<MoveDirection, Action> moveMap = new Dictionary<MoveDirection, Action>();

    // Use this for initialization
    void Start()
    {
        moveMap.Add(MoveDirection.Forward,
        () =>
        {
            transform.position += (transform.forward * Time.deltaTime * _velocity);
        });

        moveMap.Add(MoveDirection.Back,
        () =>
        {
            transform.position += (-transform.forward * Time.deltaTime * _velocity);
        });

        moveMap.Add(MoveDirection.Left,
        () =>
        {
            transform.position += (-transform.right * Time.deltaTime * _velocity);
        });

        moveMap.Add(MoveDirection.Right,
        () =>
        {
            transform.position += (transform.right * Time.deltaTime * _velocity);
        });
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (transform.position.y < -9f)
        {
            Debug.Log("Should be resetting position");
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Environment")
        //{
        //    // get highest contact point
        //    float max = collision.contacts[0].point.y;
        //    foreach (var contact in collision.contacts)
        //    {
        //        if (contact.point.y > max)
        //        {
        //            max = contact.point.y;
        //        }
        //    }

        //    // set the position to the highest contact point with plane axes unchanged
        //    transform.position = new Vector3(transform.position.x, max, transform.position.z);
        //}
    }

    private void HandleInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var jump = Input.GetAxis("Jump");

        bool isWalking = false;

        if (horizontal > 0)
        {
            isWalking = true;
            moveMap[MoveDirection.Right]();
        }
        else if (horizontal < 0)
        {
            isWalking = true;
            moveMap[MoveDirection.Left]();
        }

        if (vertical > 0)
        {
            isWalking = true;
            moveMap[MoveDirection.Forward]();
        }
        else if (vertical < 0)
        {
            isWalking = true;
            moveMap[MoveDirection.Back]();
        }

        if (jump != 0)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * _jumpForce, ForceMode.Force);
        }

        GetComponent<Animator>().SetBool("Walking", isWalking);

        // rotate the player according to camera direction
        Vector3 cameraDirection = Camera.main.transform.forward.normalized;
        Vector3 playerDirection = Vector3.ProjectOnPlane(cameraDirection, transform.up).normalized;
        transform.LookAt(playerDirection + transform.position);
    }
}
