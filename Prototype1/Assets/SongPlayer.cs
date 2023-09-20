using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BeatState { Inactive, OnBeat, OffBeat}

[RequireComponent(typeof(AudioSource))]
public class SongPlayer : MonoBehaviour
{
    public static SongPlayer Instance;

    public BeatState beatState;
    public double bpm;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
    public double inputBufScale = 0.58f;
    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;
    AudioSource audioSource;


    double quarterInputBuffer;
    double samplesPerTick;
    double sample;
    int quarterNotesPassed;


    private double audioDspStartTime;
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        accent = signatureHi;
        sampleRate = AudioSettings.outputSampleRate;
        samplesPerTick = sampleRate * 60.0F / bpm * (4.0F / signatureLo);
        quarterInputBuffer = ((samplesPerTick * inputBufScale));
        running = true;
        double startTick = AudioSettings.dspTime;
        audioDspStartTime = AudioSettings.dspTime + 1;
        nextTick = startTick * sampleRate;
        audioSource.PlayScheduled(audioDspStartTime);
        
    }

    private void FixedUpdate()
    {
        UpdateBeatState();
    }

    public bool IsOnBeat()
    {
        if(beatState == BeatState.OnBeat)
        {
            return true;
        }
        return false;
    }

    private void UpdateBeatState()
    {
        if (IsOnQuarter())
        {
            beatState = BeatState.OnBeat;
        }
        else
        {
            beatState = BeatState.OffBeat;
        }
        //Debug.Log(beatState);
        //Debug.Log(sample);
        //Debug.Log(samplesPerTick);
    }

    private bool IsOnQuarter()
    {
        if (sample > nextTick - quarterInputBuffer
            && sample < nextTick + quarterInputBuffer)
        {
            return false;
        }
        return true;
    }

    

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                //at 120 bpm should trigger every 24000 samples
                nextTick += samplesPerTick;
                amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }

    }
}
