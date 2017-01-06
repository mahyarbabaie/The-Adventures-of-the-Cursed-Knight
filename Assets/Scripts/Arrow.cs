using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidBody;
    private Vector3 direction;

	// Use this for initialization
	private void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        myRigidBody.velocity = direction * speed;
	}

    public void InitializeArrowDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
