using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;
    Vector2 moveDirection = new Vector2(1,0);
    // Variables related to player character movement
    public InputAction talkAction;
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    public GameObject projectilePrefab;

    
    public int maxHealth = 10;
    int currentHealth ; 
    public int health { get { return currentHealth; }}
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;
    
    public static PlayerController instance;  // Singleton instance

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // Prevent duplicates
        }
    }
    void Start()
    {
        talkAction.Enable();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }
 
    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
       
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * (speed * Time.deltaTime);
        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
          
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (UIBarHandler.instance)
        {
            UIBarHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        }
        else
        {
            Debug.Log("UIBarHandler instance is null! Make sure it exists in the scene.");
        }
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);


        animator.SetTrigger("Launch");
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
}

