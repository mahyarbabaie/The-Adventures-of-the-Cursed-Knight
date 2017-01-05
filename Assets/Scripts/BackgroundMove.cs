using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Transform cam;
    public float scrollspeed;
    private Vector3 cameraOldPosition, cameraCurrentPosition;
	// Use this for initialization
	private void Start ()
    {
        cameraOldPosition = cam.position;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        // camera current position is set to the location of the camera
        cameraCurrentPosition = cam.position;
        transform.position += (scrollspeed * (cameraCurrentPosition - cameraOldPosition));
        cameraOldPosition = cameraCurrentPosition;
	}
}
