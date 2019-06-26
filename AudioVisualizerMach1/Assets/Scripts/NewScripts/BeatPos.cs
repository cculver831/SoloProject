using System;

using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class BeatPos : PropertyAttribute
{
    public readonly int patternLength;
    public BeatPos (int patterLength)
    {
        this.patternLength = patternLength;
    }
}
