using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PitchUtils
{
    public static string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

    public static float[] noteFreq =
    {
        261.626f,
        277.183f,
        293.665f,
        311.127f,
        329.628f,
        349.228f,
        369.994f,
        391.995f,
        415.305f,
        440.000f,
        466.164f,
        493.883f
    };
}