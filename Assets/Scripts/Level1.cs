using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level2");
    }
}