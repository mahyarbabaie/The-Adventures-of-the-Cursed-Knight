using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    private float speed = 1;
    private float time = 12;
    private LevelManager levelManager;
    // Update is called once per frame

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }
    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        time -= Time.deltaTime;

        if (time < 0) { levelManager.LoadLevel("01a_MainMenu"); }
        else if (Input.GetKeyDown(KeyCode.Escape)) { levelManager.LoadLevel("01a_MainMenu"); }
    }
}
