using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // the time to load next level
    public float autoLoadNextLevel;
   
    // Happens at the start of the game
    private void Start ()
    {
        // calls a function and runs it at the time 
        if (autoLoadNextLevel > 0) { Invoke("LoadNextLevel", autoLoadNextLevel); } 
 	}

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnApplicationQuit()
    {
        // Alt F4 aka quits the game
        Application.Quit();
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
	

}
