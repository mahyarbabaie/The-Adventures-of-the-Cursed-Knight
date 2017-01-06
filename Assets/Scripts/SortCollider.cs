using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortCollider : MonoBehaviour {

    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == targetTag) { GetComponent<Collider2D>().enabled = false; }
    }
}
