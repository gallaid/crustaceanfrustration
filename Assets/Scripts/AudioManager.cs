using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

// referenced for this class: http://answers.unity3d.com/questions/878382/audio-or-music-to-continue-playing-between-scene.html 

class AudioManager : MonoBehaviour
{
    static bool _audioStarted = false;

    static int _trackNumber = 0;

    public List<AudioClip> musicTracks = new List<AudioClip>();

    void Awake()
    {
        if (!_audioStarted)
        {
            GetComponent<AudioSource>().clip = musicTracks[_trackNumber];
            GetComponent<AudioSource>().Play();
            _audioStarted = true;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //if (_audioStarted)
        //{
        //    if (!GetComponent<AudioSource>().isPlaying)
        //    {
        //        _trackNumber += 1;
        //        GetComponent<AudioSource>().clip = musicTracks[_trackNumber];
        //        GetComponent<AudioSource>().Play();
        //    }
        //}
    }
}
