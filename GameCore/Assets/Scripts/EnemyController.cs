using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public AudioClip DamageClip;
    public enum State { Idle, Chasing, Attacking }
    private State currentState = State.Idle;

    public float chaseSpeed ;
    public float attackRange = 1.2f;
    public float detectionRange ;
    public float attackCooldown = 1.0f;

    private Rigidbody2D rigidbody2d;
    private Transform player;
    private PlayerController playerController;
    private float lastAttackTime;
    bool broken = false;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        // Find player
        if (PlayerController.instance != null)
        {
            player = PlayerController.instance.transform;
            playerController = PlayerController.instance;
        }
        else
        {
            Debug.LogError("PlayerController instance not found!");
        }
    }

    void FixedUpdate()
    {
        if(broken)
        {
            return;
        }
        HandleState();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case State.Idle:
                rigidbody2d.linearVelocity = Vector2.zero;  // Stop movement
                break;
            case State.Chasing:
                ChasePlayer();
                break;
            case State.Attacking:
                AttackPlayer();
                break;
        }

        UpdateState();
    }

    void ChasePlayer()
    {
        if (!player) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rigidbody2d.linearVelocity = direction * chaseSpeed;
    }

    void AttackPlayer()
    {
        
        playerController.PlaySound(DamageClip);
        if (!playerController) return;

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            playerController.ChangeHealth(-3);
            lastAttackTime = Time.time;

            if (playerController.health > 0)
            {
                Destroy(gameObject);  // Enemy destroys itself
            }
            else
            {
                Debug.Log("Player Died! Enemy Wins.");
            }
        }
    }

    void UpdateState()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < attackRange)
        {
            currentState = State.Attacking;
        }
        else if (distance < detectionRange)
        {
            currentState = State.Chasing;
        }
        else
        {
            currentState = State.Idle;
        }
    }
    
    public void Fix()
    {
        broken = true;
        Destroy(gameObject);
    }
}
