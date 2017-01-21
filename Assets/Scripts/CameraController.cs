using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Vector3 _startingRelativePosition = new Vector3(0, 4, -3);

	public float rotationVelocity = 5f;

	private GameObject playerObject;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
		transform.parent = playerObject.transform;
		transform.position = _startingRelativePosition;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}

	private void HandleInput() {
		float horizontal = Input.GetAxis("HorizontalAxisRotation");
		float vertical = Input.GetAxis("VerticalAxisRotation");
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		if (horizontal < 0 || mouseY > 0) {
			RotateDown();
		}
		else if (horizontal > 0 || mouseY < 0) {
			RotateUp();
		}

		if (vertical < 0 || mouseX < 0) {
			RotateLeft();
		}
		else if (vertical > 0 || mouseX > 0) {
			RotateRight();
		}
	}

	private void RotateLeft() {
		playerObject.transform.Rotate(-playerObject.transform.up * rotationVelocity * Time.deltaTime);
	}

	private void RotateRight() {
		playerObject.transform.Rotate(playerObject.transform.up * rotationVelocity * Time.deltaTime);
	}

	private void RotateUp() {
		transform.RotateAround(playerObject.transform.position,
							   playerObject.transform.right,
							   Time.deltaTime * rotationVelocity);
	}

	private void RotateDown() {
		transform.RotateAround(playerObject.transform.position,
							   playerObject.transform.right,
							   -Time.deltaTime * rotationVelocity);	
	}
}
