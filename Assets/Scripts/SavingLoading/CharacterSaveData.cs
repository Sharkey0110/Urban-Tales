using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this isnt going to be attached to anything, it is a refrence, so no mono behavior and add system

[System.Serializable]
public class CharacterSaveData
{
    //what do we need to save

    [Header("Name")]
    public string characterName;

    [Header("Time Played")]
    public int secondsPlayed;

    [Header("World Coordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;
    public int scenePosition;

}
