using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    [SerializeField] public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    //     QualitySettings.vSyncCount = 0;
    //     Application.targetFrameRate = 10;
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Vector2 position = (Vector2)transform.position + move * (speed * Time.deltaTime);
        transform.position = position;
    }
}
