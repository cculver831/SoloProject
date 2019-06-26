using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


public class BeatSequencer : MonoBehaviour
{
    public bool UpdateinPlaymode;
    [BeatPattern(32, 8)]
    public string[] PatternD8;
    [BeatPos(32)]
    public int countD8;
    private int countD8LastFrame;
    [HideInInspector]
    public bool patternD8Complete;
    public bool[][] patternD8Bool;
    private string[] patternD8String;
    void Start()
    {
        patternD8Bool = new bool[PatternD8.Length][];
        SetPatternBool(PatternD8, patternD8Bool, false, 0);
        patternD8String = new string[PatternD8.Length];
    }
    void SetPatternBool(string[] beatPattern, bool[][] boolPattern, bool specificPattern, int SpecficIndex)
    {
        if (!specificPattern)
        {
            for (int i = 0; i < beatPattern.Length; i++)
            {
                boolPattern[i] = new bool[beatPattern[i].Length];
                StringBuilder sb = new StringBuilder(beatPattern[i]);
                for (int j = 0; j < sb.Length; j++)
                {
                    if (sb[j] == '1')
                    {
                        boolPattern[i][j] = true;
                    }
                    else
                    {
                        boolPattern[i][j] = false;
                    }
                }
            }
        }
        if (specificPattern)
        {
            StringBuilder sb = new StringBuilder(beatPattern[SpecficIndex]);
            for (int j = 0; j < sb.Length; j++)
            {
                if (sb[j] == '1')
                {
                    boolPattern[SpecficIndex][j] = true;
                }
                else
                {
                    boolPattern[SpecficIndex][j] = false;
                }
            }

        }

        void Update()
        {
            CheckPatternCompleted();
            if (UpdateinPlaymode)
            {
                for (int i = 0; i < PatternD8.Length; i++)
                {
                    if (patternD8String[i] != PatternD8[i])
                    {
                        SetPatternBool(PatternD8, patternD8Bool, true, i);
                        patternD8String[i] = PatternD8[i];
                    }
                }
            }
        }
        void CheckPatternCompleted()
        {
            if (patternD8Complete) { patternD8Complete = false; }
            countD8LastFrame = countD8;
            countD8 = BPMSequencer.BeatCountD8 % 32;

            if (countD8 == 0 && countD8LastFrame != countD8)
            {
                patternD8Complete = true;
            }
        }
    }
}