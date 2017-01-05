using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player") { Destroy(gameObject); }
    }

}
