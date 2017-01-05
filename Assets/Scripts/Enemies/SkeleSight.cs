using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleSight : MonoBehaviour
{
    [SerializeField]
    private Skele skele;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player") { skele.Target = collider.gameObject; }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player") { skele.Target = null; }
    }

}
