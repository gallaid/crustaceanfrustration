using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class SFXManager : MonoBehaviour
{
    public List<AudioClip> _sfxClips = new List<AudioClip>();

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void PlayRandomEffect()
    {
        GetComponent<AudioSource>().clip = _sfxClips[UnityEngine.Random.Range(0, _sfxClips.Count)];
        GetComponent<AudioSource>().Play();
    }
}