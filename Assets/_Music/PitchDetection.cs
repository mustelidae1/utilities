using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class PitchDetection : MonoBehaviour
{
    public static PitchDetection s;
    public bool manualOverride = false;

    [DllImport("AudioPluginDemo")]
    private static extern float PitchDetectorGetFreq(int index);

    [DllImport("AudioPluginDemo")]
    private static extern int PitchDetectorDebug(float[] data);

    float[] history = new float[1000];
    float[] debug = new float[65536];

    //public Material mat;
    public string frequency = "detected frequency";
    public float frequencyFloat = 0;
    public string note = "detected note";


    public int prevNoteIndex = 0;

    [Range(0, 11)]
    public int m_noteIndex = 0;
    public AudioMixer mixer;

    public Text text;

    void Start()
    {
        s = this;
    }

    void Update()
    {
        float freq = PitchDetectorGetFreq(0), deviation = 0.0f;
        frequencyFloat = freq;
        frequency = freq.ToString() + " Hz";

        if (freq > 0.0f)
        {
            float noteval = 57.0f + 12.0f * Mathf.Log10(freq / 440.0f) / Mathf.Log10(2.0f);
            float f = Mathf.Floor(noteval + 0.5f);
            deviation = Mathf.Floor((noteval - f) * 100.0f);
            int noteIndex = (int)f % 12;
            if (!manualOverride)
            {
                m_noteIndex = noteIndex;

            }

            if (m_noteIndex != prevNoteIndex)  // Note has changed 
            {
                prevNoteIndex = m_noteIndex;
            }
            int octave = (int)Mathf.Floor((noteval + 0.5f) / 12.0f);
            note = PitchUtils.noteNames[noteIndex] + " " + octave;
        }
        else
        {
            note = "unknown";
        }

        if (text != null)
            text.text = "Detected frequency: " + frequency + "\nDetected note: " + note + " (deviation: " + deviation + " cents)";
    }
}

