using System;
using UnityEngine;

public class TimedAction : MonoBehaviour
{

    public float timer;

    public Action _action;

    private bool done = false;

    void Start()
    {
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !done)
        {
            _action();
            done = true;
        }
    }
}