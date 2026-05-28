using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{
    public string menuSceneName = "TitleScreen";
    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
