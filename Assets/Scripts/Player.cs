using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();

public class Player : MonoBehaviour
{
    public event DeadEventHandler Dead;
    [SerializeField]
    private float immortalTime;
    [SerializeField]
    private Stats healthStat;
    [SerializeField]
    private EdgeCollider2D swordCollider;
    [SerializeField]
    private List<string> damageSources;
    [SerializeField]
    private float movementSpeed;

    // private variables
    private Vector3 startPos;
    private SpriteRenderer spriteRenderer;
    private IUseable useable;
    private Transform currentLocation;
    private static Player instance;
    private bool isDead;
    private bool immortal = false;
    private bool facingRight;
  
    // Properties
    public Rigidbody2D MyRigidbody { get; set; }
    public Animator MyAnimator { get; set; }
    public EdgeCollider2D SwordCollider { get { return swordCollider; } }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    public bool OnHeart { get; set; }

    private void Awake()
    {
        healthStat.Initialize();
    }

    // Use this for initialization
    private void Start ()
    {
        InitializingStartValues();
	}

    private void Update()
    {
        HandleInput();
    }
    // using FixedUpdate to deal with physics
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        HandleMovement(horizontal, vertical);
        Flip(horizontal);
    }

    // creates a singleton of the player
    public static Player Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<Player>(); }
            return instance;
        }
    }

    // Check to see if the player is dead
    private void OnDead()
    {
        if (Dead != null) { Dead(); }
    }

    // If the player is dead then set his hp to 0
    public bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0) { OnDead(); }
            return healthStat.CurrentVal <= 0;
        }
    }

    // Respawns the player when he dies
    public void Death()
    {
        MyRigidbody.velocity = Vector3.zero;
        MyAnimator.SetTrigger("idle");
        healthStat.CurrentVal = healthStat.MaxVal;
        transform.position = startPos;
    }

    // handles the taking damage part of the player
    public IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentVal -= 10;
            if (!IsDead)
            {
                MyAnimator.SetTrigger("takingDamage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                MyAnimator.SetTrigger("dead"); 
                Death();
            }
        }
    }

    // Blinks the player on and off
    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    // the action button for the player
    private void Use()
    {
        if (useable != null)
        {
            useable.Use();
        }
    }

    // handles player input
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Z)) { MyAnimator.SetTrigger("attack"); } 
        if (Input.GetKeyDown(KeyCode.X)) { MyAnimator.SetTrigger("block"); }
        if (Input.GetKeyDown(KeyCode.C)) { Use(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { return; } // TODO MAKE A PAUSE FUNCTION
    }

    private void HandleMovement(float horizontal, float vertical)
    {
        MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (damageSources.Contains(collider.tag)) { StartCoroutine(TakeDamage()); }
        if (collider.tag == "Useable") { useable = collider.GetComponent<IUseable>(); }  // TODO make useable tag
        // Player gets the checkpoint of the heart and heals up
        if (collider.tag == "Heart")
        {
            healthStat.CurrentVal += 100;
            startPos = collider.gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Useable") { useable = null; }
    }

    // flips the direction of the player
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) { ChangeDirection(); }
    }

    // Changes the direction of the player
    private void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void InitializingStartValues()
    {
        MyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        facingRight = true;
        OnHeart = false;
        startPos = transform.position;
    }
     


}
