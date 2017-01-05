using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider volumeSlider;
    public LevelManager levelManager;

    private MusicManager musicManager;

	// Use this for initialization
	private void Start ()
    {
        musicManager = FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
	}
	
	// Update is called once per frame
	private void Update ()
    {
        // sets the volume of the music based on what the slider gives it
        musicManager.SetVolume(volumeSlider.value);
	}

    public void SaveAndExit()
    {
        // saving the volume in the player prefs area
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        levelManager.LoadLevel("01a_MainMenu");
    }

    public void SetDefaults()
    {
        volumeSlider.value = 0.7f;
    }

    
}
