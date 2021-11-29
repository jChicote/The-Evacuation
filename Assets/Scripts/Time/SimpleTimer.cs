using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheEvacuation.TimeUtility
{
    public class SimpleCountDown
    {
        // Fields
        private float intervalLength;
        private float timeLeft;
        private float deltaTime;
        private float interpolateValue;

        // Properties
        public float TimeLeft { get => timeLeft; }
        public float InterpolateValue { get => interpolateValue;  }

        public SimpleCountDown(float intervalLength, float deltaTime)
        {
            this.intervalLength = intervalLength;
            this.timeLeft = intervalLength;
            this.deltaTime = deltaTime;
        }

        public void TickTimer()
        {
            timeLeft -= deltaTime;
            interpolateValue = 1 - timeLeft / intervalLength;
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
}
