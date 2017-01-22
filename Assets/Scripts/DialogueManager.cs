using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class DialogueManager : MonoBehaviour
{
    public List<AudioClip> dialogueClips = new List<AudioClip>();

    public float _delayMinInSeconds = 10f;

    public float _delayMaxInSeconds = 30f;

    private float _currentDelay;

    private float _timeSinceLastLine = 0f;

    void Start()
    {
        _currentDelay = UnityEngine.Random.Range(_delayMinInSeconds, _delayMaxInSeconds);
    }

    void Update()
    {
        _timeSinceLastLine += Time.deltaTime;

        if (_timeSinceLastLine >= _currentDelay)
        {
            // play random dialogue line
            int randomIndex = UnityEngine.Random.Range(0, dialogueClips.Count);
            GetComponent<AudioSource>().clip = dialogueClips[randomIndex];
            GetComponent<AudioSource>().Play();

            // set new random delay and reset accumulator
            _currentDelay = UnityEngine.Random.Range(_delayMinInSeconds, _delayMaxInSeconds);
            _timeSinceLastLine = 0;
        }
    }
}