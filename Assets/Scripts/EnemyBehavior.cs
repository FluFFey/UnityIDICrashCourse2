using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private Rigidbody2D rb2d;
    [SerializeField] private float maxHSpeed;
    [SerializeField] private float velocityIncrements;
    [SerializeField] private float jumpForce;
    bool hasJumped = true;
    bool isGrounded = false;
    [SerializeField] float maxLife;
    public float currentLife;
    [SerializeField] private LayerMask groundMask;

    private bool canMove;
    private float knockbackForce;
    private float knockbackTime;
    private bool knockedBack = false;
    private Vector2 knockBackVector;

    [SerializeField] private GameObject groundChecker;
    //-1 = left, 0 = no movement, 1 = right
    float walkingDirection = 0;
    GameObject player;
    bool wallAhead;
    
    [SerializeField] private float disabledTimer;
    float timeDisabled;
    private void Awake()
    {
        player = GameObject.Find("Player");
        rb2d = GetComponent<Rigidbody2D>();
        currentLife = maxLife;
        canMove = true;
        timeDisabled = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        wallAhead = false;
        walkingDirection = 0;
        if (player.transform.position.x > transform.position.x)
        {
            walkingDirection = 1;
        }
        if (player.transform.position.x < transform.position.x)
        {
            walkingDirection = -1;
        }
        timeDisabled += Time.deltaTime;
        if (!canMove && timeDisabled > disabledTimer)
        {
            canMove = true;
        }
    }

    private void FixedUpdate()
    {
        if (knockedBack)
        {
            canMove = false;
            rb2d.AddForce(knockBackVector, ForceMode2D.Impulse);
            knockedBack = false;
        }
        if (canMove)
        {
            

            Vector2 newVelocity = rb2d.velocity + (walkingDirection * velocityIncrements * Vector2.right * Time.fixedDeltaTime);
            if (newVelocity.x < 0 && walkingDirection > 0)
            {
                newVelocity.x *= .8f * (1 - Time.fixedDeltaTime);
            }
            if (newVelocity.x > 0 && walkingDirection < 0)
            {
                newVelocity.x *= .8f * (1 - Time.fixedDeltaTime);
            }
            newVelocity.x = Mathf.Clamp(newVelocity.x, -maxHSpeed, maxHSpeed);
            rb2d.velocity = newVelocity;
            isGrounded = checkForGroundCollisions();
            wallAhead = checkForWall((Vector2)transform.position + walkingDirection * Vector2.right * 0.5f);
            if (wallAhead && isGrounded && rb2d.velocity.y <= 0)
            {
                rb2d.AddForce(Vector2.up * jumpForce);
                hasJumped = false;
            }
        }
    }

    private bool checkForGroundCollisions()
    {
        Vector2 center = groundChecker.transform.position;
        Vector2 size = groundChecker.GetComponent<BoxCollider2D>().size;
        Collider2D allOverlappingColliders = Physics2D.OverlapBox(center, size, 0f, groundMask);
        if (allOverlappingColliders == null)
            return false;
        return true;
    }

    private bool checkForWall(Vector2 pointToCheck)
    {
        Collider2D firstOverlappingCollider = Physics2D.OverlapCircle(pointToCheck, 0.02f, groundMask);
        if (firstOverlappingCollider == null)
            return false;
        return true;
    }

    internal void dealDamage(float damage, Vector2 sourcePosition)
    {
        currentLife -= damage;
        GetComponent<ParticleSystem>().Emit(10);
        if (currentLife <=0)
        {
            Destroy(gameObject);
        }
        timeDisabled = 0;
        knockedBack = true;
        if (sourcePosition.x - transform.position.x > 0)
        {
            knockBackVector = new Vector2(-2.5f, 2f);
        }
        if (sourcePosition.x - transform.position.x < 0)
        {
            knockBackVector = new Vector2(2.5f, 2f);
        }
    }
}