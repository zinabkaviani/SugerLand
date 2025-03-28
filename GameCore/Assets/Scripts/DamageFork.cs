using UnityEngine;

public class DamageFork : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();


        if (controller != null && controller.health < 0)
        {
            controller.ChangeHealth(-1);
        }
    }
}
