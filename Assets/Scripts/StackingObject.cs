using System;
using UnityEngine;

public class StackingObject : MonoBehaviour
{

    public float _timeToFreeze = 0.2f;

    private GameObject dialogueManager;
    private GameObject sfxManager;

    // Use this for initialization
    void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager");
        sfxManager = GameObject.FindGameObjectWithTag("SFXManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (sfxManager == null || dialogueManager == null)
        {
            dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager");
            sfxManager = GameObject.FindGameObjectWithTag("SFXManager");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stackable")
        {
            Action removeRigidBodyAction = () =>
            {
                //GetComponent<Rigidbody>().isKinematic = true;
                RigidbodyConstraints newConstraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                //RigidbodyConstraints rotConstraints = RigidbodyConstraints.FreezeRotation;
                GetComponent<Rigidbody>().constraints = newConstraints;
                if (!collision.gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    collision.gameObject.GetComponent<Rigidbody>().constraints = newConstraints;
                }
            };

            var timedAction = gameObject.AddComponent(typeof(TimedAction)) as TimedAction;
            timedAction._action = removeRigidBodyAction;
            timedAction.timer = _timeToFreeze;

            if (sfxManager != null)
            {
                // too noisy
                // sfxManager.GetComponent<SFXManager>().PlayRandomEffect();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WorldEnder")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(new Vector3(10000, 10000, 10000), ForceMode.Acceleration);

        }
    }
}
