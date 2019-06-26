using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnBeat : MonoBehaviour
{
    //Usable AudioClips
    public SoundManager soundManager;
    public AudioClip tap, tick;
    public AudioClip[] strum;
    int randomStrum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BPMSequencer.beatFull)
        {
            soundManager.PlaySound(tap, 1);
            if(BPMSequencer.beatCountFull % 2 == 0)
            {
                randomStrum = Random.Range(0, strum.Length);
            }
        }
        if(BPMSequencer.beatD8 && BPMSequencer.BeatCountD8 % 2 == 0)
        {
            soundManager.PlaySound(tick, 0.1f);
        }
        if(BPMSequencer.beatD8 && (BPMSequencer.BeatCountD8 % 8 == 2 || BPMSequencer.BeatCountD8 % 8 == 4))
        {
            soundManager.PlaySound(strum[randomStrum], 1);
        }
    }
}
