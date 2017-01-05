using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    private void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
	}

    private void OnLevelWasLoaded(int level)
    {
        // sets the music to the current level variable
        AudioClip thisLevelMusic = levelMusicChangeArray[level];
        // If there is music for the current level
        if (thisLevelMusic)
        {
            // set the audio to play and loop the current level music
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        // sets the volume based on the passed in parameter
        audioSource.volume = volume;
    }

}
