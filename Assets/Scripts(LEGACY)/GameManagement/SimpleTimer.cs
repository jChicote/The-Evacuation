using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimer
{
    // Fields
    private float intervalLength;
    private float timeLeft;
    private float deltaTime;

    public SimpleTimer(float intervalLength, float deltaTime)
    {
        this.intervalLength = intervalLength;
        this.timeLeft = intervalLength;
        this.deltaTime = deltaTime;
    }

    public void TickTimer()
    {
        timeLeft -= deltaTime;
    }

    public bool CheckTimeIsUp()
    {
        return timeLeft <= 0;
    }

    public void ResetTimer()
    {
        timeLeft = intervalLength;
    }
}
