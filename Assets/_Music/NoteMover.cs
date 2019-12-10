using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{
    private GameObject spawnPoint;
    private GameObject endPoint;

    public float BPS;

    private float t;

    void Start()
    {
        t = 0;
        BPS = Spawn.s.beatsPerSecond;
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnNote");
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
    }

    void Update()
    {
        t += Time.deltaTime / (BPS * 4);
        transform.position = Vector3.Lerp(spawnPoint.transform.position, endPoint.transform.position, t);
    }
}
