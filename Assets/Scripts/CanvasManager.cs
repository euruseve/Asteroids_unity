using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }


}
