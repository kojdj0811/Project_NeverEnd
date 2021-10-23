using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTime : MonoBehaviour
{
    public static PlayTime S;

    public float StartTime;

    public float EndTime;

    public float playtime;

    private void Awake()
    {
        S = this;
    }
    void Start()
    {
        StartTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        
    }

    void Play()
    {
        playtime = EndTime - StartTime;
    }

    public void arrive()
    {

    }

    public void setendtime(float inputtime)
    {
        EndTime = inputtime;
    }
}
