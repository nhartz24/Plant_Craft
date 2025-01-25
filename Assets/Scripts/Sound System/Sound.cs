//this script creates a Sound class with certain useful variables to call from the sound manager

using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool isLoop;
    public bool hasCooldown;

    [HideInInspector]
    public AudioSource source;
}

//stolen from youtube comment section lol
//Ergin Deniz Kosecioglu the goat