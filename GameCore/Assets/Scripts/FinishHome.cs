using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    public ParticleSystem finishEffect; 
    
    public AudioClip WinClip;// Assign this in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something touched the finish: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the finish!");
            

            if (finishEffect != null)
            {
                Debug.Log("Playing Particle Effect!");
                finishEffect.Play();
            }
            else
            {
                Debug.LogError("FinishEffect is NOT assigned in Inspector!");
            }

            PlayerController playerMovement = other.GetComponent<PlayerController>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            Invoke("RestartGame", 60f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}