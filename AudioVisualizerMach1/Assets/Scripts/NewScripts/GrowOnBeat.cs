using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BeatSequencer))]
public class GrowOnBeat : MonoBehaviour
{
    BeatSequencer beatSequencer;
    [Header("Behavior Settings")]
    public Transform target;
    private float currentSize;
    public float growSize, shrinkSize;
    [Range(0.8f, 0.99f)]
    public float shrinkFactor;
    [Header("Pattern Settings")]
    public Vector2Int fromToPattern;
    private int currentPattern;
    public bool PatternRandom;


    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = this.transform;
        }
        beatSequencer = GetComponent<BeatSequencer>();
        currentSize = shrinkSize;
        currentPattern = fromToPattern.x;
    }

    // Update is called once per frame
    void Update()
    {
        SelectCurrentPattern();
        if(currentSize > shrinkSize)
        {
            currentSize *= shrinkFactor;
        }
        else
        {
            currentSize = shrinkSize;
        }
        CheckBeat();
        target.localScale = new Vector3(target.localScale.x, currentSize, target.localScale.z);
    }
    void SelectCurrentPattern()
    {
        if (beatSequencer.patternD8Complete)
        {
            if (PatternRandom)
            {
                currentPattern = Random.Range(fromToPattern.x, fromToPattern.y + 1);
            }
            else
            {
                if ( currentPattern < fromToPattern.y)
                {
                    currentPattern++;
                }
                else
                {
                    currentPattern = fromToPattern.x;
                }
            }
        }
    }
    void Grow()
    {
        currentSize = growSize;
    }
    void CheckBeat()
    {
       if(BPMSequencer.beatD8)
        {
            if(beatSequencer.patternD8Bool[currentPattern][beatSequencer.countD8])
            {
                Grow();
            }
        }
    }
}
