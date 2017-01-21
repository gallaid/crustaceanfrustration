using System;
using UnityEngine;

public class StackingObject : MonoBehaviour {

	public float _timeToFreeze = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Stackable") {
			Action removeRigidBodyAction = () => {
				GetComponent<Rigidbody>().isKinematic = true;
			};
			var timedAction = gameObject.AddComponent(typeof(TimedAction)) as TimedAction;
			timedAction._action = removeRigidBodyAction;
			timedAction.timer = _timeToFreeze;
		}
	}
}
