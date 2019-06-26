using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMSequencer : MonoBehaviour
{
    private static BPMSequencer BPM;
    public float _BPM;
    private float beatInterval, beatTimer, beatIntervalD8, beatTimerD8;
    public static bool beatFull, beatD8;
    public static  int beatCountFull, BeatCountD8;

    public float[] tapTime = new float[4];
    public static int tap;
    public static bool customBeat;

    private void Awake()
    {
        if(BPM != null && BPM != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            BPM = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BeatDetection();
        Tapping();
    }
    void Tapping()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            customBeat = true;
            tap = 0;
        }
        if (customBeat)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                tapTime[tap] = Time.realtimeSinceStartup;
                tap++;
                if(tap == 4)
                {
                    float avgTime = ((tapTime[1] - tapTime[0]) + (tapTime[2] - tapTime[1]) + (tapTime[3] - tapTime[2])) / 3;
                    _BPM = (float)System.Math.Round((double)60 / avgTime, 2);
                    tap = 0;
                    beatTimer = 0;
                    beatTimerD8 = 0;
                    beatCountFull = 0;
                    customBeat = false;
                }
            }
        }
    }
    void BeatDetection()
    {
        beatFull = false;
        beatInterval = 60 / _BPM;
        beatTimer += Time.deltaTime;
        if(beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
            Debug.Log("Full");
        }
        beatD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;
        if (beatTimerD8 >= beatIntervalD8)
        {
            beatTimerD8 -= beatIntervalD8;
            beatD8 = true;
            BeatCountD8++;
            Debug.Log(" Not Full");
        }
    }
}
