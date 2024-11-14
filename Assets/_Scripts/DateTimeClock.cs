using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateTimeClock : MonoBehaviour
{
    [SerializeField] private bool useSmoothing;
    [SerializeField] private Transform secondsPointer;
    [SerializeField] private Transform minutesPointer;
    [SerializeField] private Transform hoursPointer;

    // Update is called once per frame
    void Update()
    {
        UpdateClock(useSmoothing);
    }

    private void UpdateClock(bool useSmoothing = true)
    {
        var dt = DateTime.Now;
        var seconds = (float) dt.Second;
        var minutes = (float) dt.Minute;
        var hours = (float) dt.Hour;

        //Hours should be progressing regardless of smoothing
        hours += dt.Minute / 60f;

        if(useSmoothing)
        {
            minutes += dt.Second / 60f;
            seconds += dt.Millisecond / 1000f;
        }

        UpdatePointer(secondsPointer, seconds, 60);
        UpdatePointer(minutesPointer, minutes, 60);
        UpdatePointer(hoursPointer, hours % 12, 12);
    }

    private void UpdatePointer(
        Transform pointer, 
        float value, 
        float maxValue,
        float fullAngle = 360f)
    {
        var angle = (value / maxValue) * fullAngle;
        pointer.localEulerAngles = new Vector3(angle, 0, 0);
    }
}
