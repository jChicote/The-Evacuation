using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer: MonoBehaviour, IPausable
{
    // Fields
    public UnityEvent OnTimerComplete;

    private bool isPaused = false;
    private bool isCompleted = false;
    private bool canTick = false;

    private float startingTime;
    private float timeLeft;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isPaused || isCompleted || !canTick) return;

        RunTimer();
    }

    /// <summary>
    /// Sets the initial starting time of the timer
    /// </summary>
    public void SetTimer(float startingTime)
    {
        this.startingTime = startingTime;
        timeLeft = startingTime;
    }

    /// <summary>
    /// Returns value of the timer's remaining time
    /// </summary>
    public float GetRemainingTime()
    {
        return timeLeft;
    }

    /// <summary>
    /// Called to manually start the timer
    /// </summary>
    public void StartTimer()
    {
        canTick = true;
    }

    /// <summary>
    /// Called to perform ticking behaviours of the timer
    /// </summary>
    public void RunTimer()
    {
        timeLeft -= Time.fixedDeltaTime;

        if (timeLeft <= 0)
            CompleteTimer();
    }

    /// <summary>
    /// Called to complete the time of the timer and any completion tasks.
    /// </summary>
    private void CompleteTimer()
    {
        canTick = false;
        isCompleted = true;
        OnTimerComplete.Invoke();
    }

    /// <summary>
    /// Resets the timer to its original defaults.
    /// </summary>
    public void ResetTimer()
    {
        timeLeft = startingTime;
        canTick = false;
    }

    // ###################################################################
    // Pausible Methods
    // ###################################################################

    public void OnPause()
    {
        isPaused = true;
    }

    public void OnUnpause()
    {
        isPaused = false;
    }
}
