using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void StartGame()
    {
        // Replace "GameScene" with your actual game scene name
        SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("GameUI");
    }
}
