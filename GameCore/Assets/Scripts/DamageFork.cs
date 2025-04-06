using UnityEngine;

public class DamageFork : MonoBehaviour
{
    public AudioClip DamageClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null )
        {
            playerController.PlaySound(DamageClip);
            playerController.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }
}
