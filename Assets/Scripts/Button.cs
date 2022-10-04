using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void Ingame()
    {
        SceneManager.LoadScene("Scenes/Ingame");
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("Scenes/Start");
    }
}