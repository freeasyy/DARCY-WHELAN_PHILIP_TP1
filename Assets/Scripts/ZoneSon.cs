using UnityEngine;

public class ZoneSon : MonoBehaviour
{
    public AudioSource audioSource;
    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (audioSource != null)
                audioSource.Play();
        }
    }
}