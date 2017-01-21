using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float _velocity = 1;

	private enum MoveDirection {
		Forward,
		Back,
		Left,
		Right
	}

	private Dictionary<MoveDirection, Action> moveMap = new Dictionary<MoveDirection, Action>();

	// Use this for initialization
	void Start () {
		moveMap.Add(MoveDirection.Forward,
		() => {
			transform.position += (transform.forward * Time.deltaTime * _velocity);
		});
		
		moveMap.Add(MoveDirection.Back,
		() => {
			transform.position += (-transform.forward * Time.deltaTime * _velocity);
		});
		
		moveMap.Add(MoveDirection.Left,
		() => {
			transform.position += (-transform.right * Time.deltaTime * _velocity);
		});
		
		moveMap.Add(MoveDirection.Right,
		() => {
			transform.position += (transform.right * Time.deltaTime * _velocity);
		});
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();	
	}

	private void HandleInput() {
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		if (horizontal > 0) {
			moveMap[MoveDirection.Right]();
		}
		else if (horizontal < 0) {
			moveMap[MoveDirection.Left]();
		}

		if (vertical > 0) {
			moveMap[MoveDirection.Forward]();
		}
		else if (vertical < 0) {
			moveMap[MoveDirection.Back]();
		}
	}
}
