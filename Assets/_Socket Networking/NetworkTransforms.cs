using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    public static TransformManager s;
    private CharacterTransforms chars;  // serializable object

    public GameObject player;

    private float sendCount;
    private void Start()
    {
        s = this;
        chars = new CharacterTransforms();
        sendCount = 0;
    }

    private void Update()
    {
        sendCount += Time.deltaTime;
        if (sendCount > 1f)
        {
            sendCount = 0;
            updatePlayerPos(player.transform.position);
        }
    }

    public void updatePlayerPos(Vector3 pos)
    {
        chars.playerPosTransform = pos;
        sendCharacters();
    }

    // Sending info to server 
    public void sendCharacters()
    {
        string json = JsonUtility.ToJson(chars);
        ServerConnect.s.sendInfo(json);
    }
}

