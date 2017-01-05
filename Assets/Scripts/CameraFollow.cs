using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float xMax, yMax, xMin, yMin;
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
    }
    private void LateUpdate ()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, xMin, xMax),
            Mathf.Clamp(target.position.y, yMin, yMax),
            transform.position.z
            );
    }
}
