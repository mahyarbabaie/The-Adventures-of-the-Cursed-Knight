using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skele : MonoBehaviour
{
    [SerializeField]
    private List<string> damageSources;
    [SerializeField]
    private Stats healthStat;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private EdgeCollider2D swordCollider;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;
    [SerializeField]
    private float meleeRange;

    // private variables
    private Canvas healthCanvas;
    private SpriteRenderer spriteRenderer;
    private Vector2 startPos;
    private ISkeleState currentState;
    private bool facingRight;

    // properties
    public GameObject Target { get; set; }
    public Rigidbody2D MyRigidbody { get; set; }
    public Animator MyAnimator { get; set; }
    public EdgeCollider2D SwordCollider { get { return swordCollider; } }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    public bool IsDead { get { return healthStat.CurrentVal <= 0; } }
    public bool InMeleeRange
    {
        get
        {
            if (Target != null) { return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange; }
            return false;
        }
    }

    // Use this for initialization
    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (damageSources.Contains(collider.tag)) { StartCoroutine(TakeDamage()); }
        currentState.OnTriggerEnter(collider);
    }

    private IEnumerator TakeDamage()
    {
        if (!healthCanvas.isActiveAndEnabled) { healthCanvas.enabled = true; }
        healthStat.CurrentVal -= 10;
        if (!IsDead)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        else
        {
            MyAnimator.SetTrigger("death");
            yield return new WaitForSeconds(3);
            Death();
            yield return null;
        }
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDirection = Target.transform.position.x - transform.position.x;
            if (xDirection > 0 && facingRight || xDirection < 0 && !facingRight) { ChangeDirection(); }
        }
    }

    private void Initialization()
    {
        facingRight = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyAnimator = GetComponent<Animator>();
        healthStat.Initialize();
        startPos = transform.position;
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new SkeleIdleState());
        healthCanvas = transform.GetComponentInChildren<Canvas>();
    }

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new SkelePatrolState());
    }

    public void ChangeState(ISkeleState newState)
    {
        if (currentState != null) { currentState.Exit(); }
        currentState = newState;
        currentState.Enter(this);
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void Move()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);
                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            else if (currentState is SkelePatrolState)
            {
                ChangeDirection();
            }
            else if (currentState is SkeleMeleeState)
            {
                Target = null;
                ChangeState(new SkeleIdleState);
            }
        }
    }

    public void Death()
    {
        healthCanvas.enabled = false;
        Destroy(gameObject);
    }

    public void ChangeDirection()
    {
        // save current enemy canvas before the enemy changes direction and then turn it off
        Transform temp = transform.FindChild("SkeleCanvas").transform;
        Vector3 after = temp.position;
        temp.SetParent(null);
        // change enemy direction
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        // once the enemy changes direction, then turn the canvas back on
        temp.SetParent(transform);
        temp.position = after;
    }

    public void MeleeAttack()
    {
        SwordCollider.enabled = true;
    }

    


    
   
}
