using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] notePrefab;
    private GameObject spawnPoint;
    private GameObject endPoint;

    [Header("Song Settings")]
    public int tempoBPM; // use this to make sure notes happen on beat
    [Range(1, 100)]
    public int activityLevel; // percent liklihood a note will be played on any given beat - probably don't need this 

    public float beatsPerSecond;
    float nextBeatCounter = 0;

    private int activityLevelIncreaseCount = 0;
    private int maxActivityLevel = 50;

    bool firstNote = true;

    Queue activeNotes;

    public static Spawn s;

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnNote");
        //30/BPM = eight notes, 60 for quarter and 15 for 16th
        beatsPerSecond = 30f / tempoBPM;
        activeNotes = new Queue();

        s = this;
    }

    void FixedUpdate()
    {
        beatsPerSecond = 30f / tempoBPM; // calculated here so we can change the tempo in the middle of the song
        int rand = Random.Range(1, 100);

        // we have reached a beat
        if (nextBeatCounter >= beatsPerSecond)
        {
            if (rand < activityLevel)
            {
                //spawn
                int note = Random.Range(0, 4); // PitchUtils.getRandomNoteIndex(mode);   // TODO: This is where we set what note we want 
                spawnNote(note);
            }

            // reset beat counter
            nextBeatCounter = 0;
        }

        nextBeatCounter += Time.deltaTime;
    }

    public GameObject spawnNote(int note)
    {
        //Spawn the note associated with the random number passed in.
        GameObject newNote = Instantiate(notePrefab[note]);
        newNote.transform.position = spawnPoint.transform.position;

        activeNotes.Enqueue(newNote);

        return null;
    }

    public GameObject getClosestNote()
    {
        // dequeue from the queue and return that 
        return null;
    }
}