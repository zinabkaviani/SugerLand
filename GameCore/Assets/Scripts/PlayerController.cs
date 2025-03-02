using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed;
        transform.position = position;
    }
}
