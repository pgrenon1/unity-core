using System;
using UnityEngine;

public class Duration
{
    public float timeDuration;

    public bool IsStarted { get; private set; } = false;
    public bool IsElapsed { get; private set; } = false;

    public float TimeLeft { get { return timeDuration - TimeElapsed; } }
    public float TimeElapsed { get; private set; }

    public TimeSpan TimeLeftTimeSpan { get { return TimeSpan.FromSeconds(TimeLeft); } }
    public TimeSpan TimeElapsedTimeSpan { get { return TimeSpan.FromSeconds(TimeElapsed); } }

    public delegate void OnDurationElapsed();
    public event OnDurationElapsed DurationElapsed;

    public Duration(float timeDuration)
    {
        this.timeDuration = timeDuration;
    }

    public void Start()
    {
        IsElapsed = false;
        IsStarted = true;
        TimeElapsed = 0f;
    }

    public void Update()
    {
        if (!IsStarted || IsElapsed)
            return;

        TimeElapsed += Time.deltaTime;

        if (TimeElapsed >= timeDuration)
        {
            IsElapsed = true;

            if (DurationElapsed != null)
                DurationElapsed();
        }
    }
}