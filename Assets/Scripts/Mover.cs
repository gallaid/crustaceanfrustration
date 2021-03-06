﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mover : MonoBehaviour
{

    public float _jumpForce = 10;

    public float _velocity = 1;

    private Vector3 _nextPosition;

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
        _nextPosition = transform.position;

        moveMap.Add(MoveDirection.Forward,
        () =>
        {
            _nextPosition += (transform.forward * Time.deltaTime * _velocity);
        });

        moveMap.Add(MoveDirection.Back,
        () =>
        {
            _nextPosition += (-transform.forward * Time.deltaTime * _velocity);
        });

        moveMap.Add(MoveDirection.Left,
        () =>
        {
            _nextPosition += (-transform.right * Time.deltaTime * _velocity);
        });

        moveMap.Add(MoveDirection.Right,
        () =>
        {
            _nextPosition += (transform.right * Time.deltaTime * _velocity);
        });
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (transform.position.y < -9f)
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void HandleInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var jump = Input.GetAxis("Jump");
        var quit = Input.GetAxis("Quit");

        bool isWalking = false;

        _nextPosition = transform.position;

        if (quit != 0)
        {
            SceneManager.LoadScene(0);
        }

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

        GetComponent<Rigidbody>().MovePosition(_nextPosition);

        // rotate the player according to camera direction
        Vector3 cameraDirection = Camera.main.transform.forward.normalized;
        Vector3 playerDirection = Vector3.ProjectOnPlane(cameraDirection, transform.up).normalized;
        transform.LookAt(playerDirection + transform.position);
    }
}
