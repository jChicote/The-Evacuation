using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer: MonoBehaviour, IPausable
{
    public UnityEvent OnTimerComplete;

    private bool isPaused = false;
    private bool isCompleted = false;
    private bool canTick = false;

    private float startingTime;
    private float timeLeft;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isPaused && isCompleted && !canTick) return;

        RunTimer();
    }

    public void SetTimer(float startingTime)
    {
        this.startingTime = startingTime;
        timeLeft = startingTime;
    }

    public void StartTimer()
    {
        canTick = true;
    }

    public void RunTimer()
    {
        timeLeft -= Time.fixedDeltaTime;

        if (timeLeft <= 0)
            CompleteTimer();
    }

    private void CompleteTimer()
    {
        canTick = false;
        isCompleted = true;
    }

    public void ResetTimer()
    {
        timeLeft = startingTime;
        canTick = false;
    }

    public void OnPause()
    {
        isPaused = true;
    }

    public void OnUnpause()
    {
        isPaused = false;
    }
}
