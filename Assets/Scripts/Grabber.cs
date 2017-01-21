using UnityEngine;

public class Grabber : MonoBehaviour {

    public Vector3 _relativePosition = new Vector3(0, 0, 0);

    public float _forceMagnitue = 10f;

    private bool _objectInBounds = false;

    private int _numInBounds = 0;

    public GameObject ObjectBeingHeld {
        get;
        private set;
    }

    public GameObject _mostRecentObject {
        get;
        private set;
    }
    
    private const string _inputAxis = "Fire1"; 

    private const string _dropAxis = "Fire2";

    private const string _throwAxis = "Fire3";

    private bool _currentlyHolding = false;
    
    void Start() {
     
    }

    void Update() {
        HandleInput();    
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Stackable") {
            _numInBounds += 1;
            _objectInBounds = other.gameObject;
            _mostRecentObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Stackable") {
            _numInBounds -= 1;
            if (other.gameObject == _mostRecentObject) {
                _mostRecentObject = null;
            }
        }
    }

    private void HandleInput() {
        float grabAxis = Input.GetAxis(_inputAxis);

        float dropAxisValue = Input.GetAxis(_dropAxis);

        float throwAxisValue = Input.GetAxis(_throwAxis);
        
        // handle button for picking up object
        if (grabAxis > 0) {
            if (_mostRecentObject != null && !_currentlyHolding)  {
                PickUpObject(); 
            }
        }

        // handle button for dropping object
        if (dropAxisValue > 0) {
            if (ObjectBeingHeld != null && _currentlyHolding) {
                DropObject();
            }
        }

        // handle button for throwing object
        if (throwAxisValue > 0) {
            if (ObjectBeingHeld != null && _currentlyHolding) {
                ThrowObject();
            }
        }
    }

    private void PickUpObject() {
        // set reference to object being held currently
        ObjectBeingHeld = _mostRecentObject;
        
        // set object as parent
        ObjectBeingHeld.transform.parent = transform;
        
        // set relative position
        ObjectBeingHeld.transform.position = _relativePosition;

        // make it kinematic for the time being
        ObjectBeingHeld.GetComponent<Rigidbody>().isKinematic = true;

        // make sure we know we can't pick anything else up until we drop this thing
        _currentlyHolding = true;
    }

    private void DropObject() {
        // release object from parent
        ObjectBeingHeld.transform.parent = null;
        
        // make it not kinematic
        ObjectBeingHeld.GetComponent<Rigidbody>().isKinematic = false;
        
        // make sure we aren't holding the object so we can pick up stuff again
        _currentlyHolding = false;

        // release the reference
        _mostRecentObject = null;
        ObjectBeingHeld = null;
    }

    private void ThrowObject() {
        // direction we'll throw the object is the forward vector of the camera
        Vector3 throwDirection = Camera.main.transform.forward.normalized;

        // make it not kinematic
        ObjectBeingHeld.GetComponent<Rigidbody>().isKinematic = false;

        // apply force to the object
        ObjectBeingHeld.GetComponent<Rigidbody>().AddForce(throwDirection * _forceMagnitue, ForceMode.Force);

        _currentlyHolding = false;
        
        // release the references
        _mostRecentObject = null;
        ObjectBeingHeld = null;
    }
}