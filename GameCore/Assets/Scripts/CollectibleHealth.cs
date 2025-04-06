using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    public AudioClip collectedClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null && playerController.health < playerController.maxHealth)
        {
            playerController.PlaySound(collectedClip);
            playerController.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}
