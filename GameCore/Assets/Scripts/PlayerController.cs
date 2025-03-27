using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private Vector2 move;
    public InputAction MoveAction;
    [SerializeField] public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        
        Vector2 position = (Vector2)rigidbody2d.position + move * (3.0f * Time.deltaTime);
        rigidbody2d.MovePosition(position);
    }
}
