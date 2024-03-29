﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof(AudioSource))]
public class Audio : MonoBehaviour { 
   AudioSource _audioSource;
    // Microphone inut
public AudioClip _AudioClip;
public bool _useMicrophone;
    public AudioMixerGroup _microphone, master;
public string selectedDevice;
public static float[] samples = new float[512];
public static float[] frequencyBand = new float[8];
public static float[] bandBuffer = new float[8];
//buffers the change in cube size to make it less sharp
float[] bufferDecrease = new float[8];
//changes light intesnisty
float[] freqbandHigh = new float[8];

public static float[] audioBand = new float[8];
public static float[] audioBandBuffer = new float[8];

// Start is called before the first frame update
void Start()
{
    _audioSource = GetComponent<AudioSource>();
    if (_useMicrophone)
    {
        if (Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
                _audioSource.outputAudioMixerGroup = _microphone;
                _audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, AudioSettings.outputSampleRate);
        

            }
        else
        {
                _useMicrophone = false;
        }
    }
    if (!_useMicrophone)
    {
        _audioSource.clip = _AudioClip;
            _audioSource.outputAudioMixerGroup = master;
        }
        _audioSource.Play();
}

// Update is called once per frame
void Update()
{
    GetSpectrumAudioSource();
    MakeFreqBands();
    BandBuffer();
    CreateAudioBands();
}
void BandBuffer()
{
    for (int g = 0; g < 8; g++)
    {
        if (frequencyBand[g] > bandBuffer[g])
        {
            bandBuffer[g] = frequencyBand[g];
            bufferDecrease[g] = 0.005f;
        }
        if (frequencyBand[g] < bandBuffer[g])
        {
            bandBuffer[g] -= bufferDecrease[g];
            bufferDecrease[g] *= 1.2f;
        }
    }
}
void CreateAudioBands()
{
    for (int i = 0; i < 8; i++)
    {
        if (frequencyBand[i] > freqbandHigh[i])
        {
            freqbandHigh[i] = frequencyBand[i];
        }
        audioBand[i] = (frequencyBand[i] / freqbandHigh[i]);
        audioBandBuffer[i] = (bandBuffer[i] / freqbandHigh[i]);
    }
}
void GetSpectrumAudioSource()
{
    _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
}
void MakeFreqBands()
{/*
        This part of the code spilts up the incoming audo based on hertz into audiobands, there are 8 total, but the plan is to change this so they wil be split up based on note A-G which goes to 11 bands total (including sharps/ flats)
        */ 
    /*
     * 220050/ 512 = 43hertz per sample
     * 20- 60
     * 60-250
     * 250- 500
     * 500-2000
     * 2000-4000
     * 4000-6000
     * 6000-20000
     * 
     * 0 - 2 samples = 86 herts
     * 1 - 4 samples = 172 hertz
     * 2 - 8
     * 3 - 16
     * 4 - 32
     * 5 - 64
     * 6 - 128
     * 7 - 256
     */
    int count = 0;
    for (int i = 0; i < 8; i++)
    {
        float avg = 0;
        // count will go to power of 0 leaving 1*2 
        int sampleCount = (int)Mathf.Pow(2, i) * 2;

        if (i == 7)
        {
            sampleCount += 2;
        }
        for (int j = 0; j < sampleCount; j++)
        {
            avg = samples[count] * (count + 1);
            count++;
        }
        avg /= count;
        frequencyBand[i] = avg * 10;
    }
}

}