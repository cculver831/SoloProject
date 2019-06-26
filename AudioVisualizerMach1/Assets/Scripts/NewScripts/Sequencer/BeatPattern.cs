using System;

using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class BeatPattern : PropertyAttribute 
{
    public readonly int patternLength;
    public readonly int beatInterval;

    public BeatPattern ( int patternLength, int beatInterval)
    {
        this.patternLength = patternLength;
        this.beatInterval = beatInterval;

    }
}
